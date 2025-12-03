using eMoody.Data.Configuration;
using eMoody.Shared.Interfaces;
using eMoody.Data.Implementations;
using eMoody.Data.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using eMoody.Server.Biz;

namespace eMoody
{
    public class Program
    {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.InjestExtraConfigs();


            // Configure for reverse proxy environment
            builder.Services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | 
                                           ForwardedHeaders.XForwardedProto | 
                                           ForwardedHeaders.XForwardedHost;

                // Trust all private/internal network proxies. We must assume the upstream infrastructure is setup correctly
                options.KnownNetworks.AddLocalNetworks(); 

                // Required for proper reverse proxy operation
                options.RequireHeaderSymmetry = false;
                options.ForwardLimit = 1; // should only ever have 1 proxy. Adjust if Cloudflare (...et al...) is used.
                });

            // Add services to the container
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Add health checks - lightweight for reverse proxy monitoring
            builder.Services.AddHealthChecks();

            // Add your services
            builder.Services.AddSingleton<BibleConfig>(p => builder.Configuration.GetSection(BibleConfig.ConfigKey).Get<BibleConfig>());
            builder.Services.AddTransient<iDataAccess, DataAccess>();

            var app = builder.Build();


            // IMPORTANT: The X-Forward-* headers are from the proxy. They must be first in the pipeline.
            app.UseOnlyLocalForwardedHeaders(); // validate X-Forward-* headers
            app.UseForwardedHeaders();          // include the X-Forward-* headers in the request


            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())  {
                app.UseWebAssemblyDebugging();
            }  else  {
                app.UseExceptionHandler("/Error");
                // Use HSTS - reverse proxy will handle HTTPS but this sets the header
                app.UseHsts();
            }

            // DO NOT use UseHttpsRedirection() - reverse proxy handles this
            // The forwarded headers will make Request.IsHttps work correctly

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();



            // Extra Endpoint: Optimized health check for reverse proxy monitoring
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        report.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy"
                    );
                },
                Predicate = _ => true,
                AllowCachingResponses = false
            });

            // Extra Endpoint: Reverse proxy monitoring
            app.MapGet("/ready", () => Results.Ok("Ready"));



            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }

    }
}