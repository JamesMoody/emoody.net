# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
# Only expose HTTP port - HTTPS handled by reverse proxy
EXPOSE 80

# Create non-root user for security
RUN addgroup --system --gid 1001 blazorgroup && \
    adduser --system --uid 1001 --gid 1001 blazoruser

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files with proper layer caching
COPY ["eMoody/Server/eMoody.Server.csproj", "eMoody/Server/"]
COPY ["eMoody/Client/eMoody.Client.csproj", "eMoody/Client/"]
COPY ["eMoody/Shared/eMoody.Shared.csproj", "eMoody/Shared/"]
COPY ["eMoody/Data/eMoody.Data.csproj", "eMoody/Data/"]

# Restore packages in separate layer for better caching
RUN dotnet restore "./eMoody/Server/eMoody.Server.csproj"

# Copy source code
COPY . .
WORKDIR "/src/eMoody/Server"

# Build with optimizations
RUN dotnet build "./eMoody.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build --verbosity minimal

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eMoody.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false \
    /p:PublishSingleFile=false \
    /p:EnableCompressionInSingleFile=true \
    /p:DebuggerSupport=false \
    /p:EnableUnsafeBinaryFormatterSerialization=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy published files with correct ownership
COPY --from=publish --chown=blazoruser:blazorgroup /app/publish .

# Set environment variables optimized for reverse proxy
ENV ASPNETCORE_ENVIRONMENT=Production
# Only listen on HTTP - reverse proxy handles HTTPS
ENV ASPNETCORE_URLS=http://+:80
# Enable forwarded headers for proper client IP and protocol detection
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
# Disable HTTPS redirection - handled by reverse proxy
ENV ASPNETCORE_HTTPS_REDIRECT_ENABLED=false
# Keep HTTPS port for URL generation
ENV ASPNETCORE_HTTPS_PORT=443

# Switch to non-root user
USER blazoruser

# Health check optimized for reverse proxy
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "eMoody.Server.dll"]