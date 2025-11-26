using eMoody.Data.Configuration;
using eMoody.Shared.Interfaces;
using eMoody.Data.Implementations;
using eMoody.Data.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eMoody
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.InjestExtraConfigs();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Add health checks - no detailed information exposed
            builder.Services.AddHealthChecks();

            // add services
            builder.Services.AddSingleton<BibleConfig>(p => builder.Configuration.GetSection(BibleConfig.ConfigKey).Get<BibleConfig>());
            builder.Services.AddTransient<iDataAccess, DataAccess>();  // the data access object. note: BibleConfig is injected into it. 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseWebAssemblyDebugging();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Add secure health check endpoint - only returns HTTP status codes
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                // Only return simple status text, no debugging information
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "text/plain";
                    // Only return "Healthy" or "Unhealthy" - no details
                    await context.Response.WriteAsync(
                        report.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy"
                    );
                },
                // Don't include any detailed health check information
                Predicate = _ => true,
                // Set appropriate cache headers
                AllowCachingResponses = false
            });

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}