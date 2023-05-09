using Microsoft.AspNetCore.ResponseCompression;
using eMoody.Server.Biz;
using Microsoft.Extensions.Configuration;
using eMoody.Config;
using eMoody.Infrastructure;
using eMoody.DAO;
using Microsoft.Extensions.Hosting;


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

            // add services
            builder.Services.AddSingleton<BibleConfig>(p => builder.Configuration.GetSection(BibleConfig.ConfigKey).Get<BibleConfig>());
            builder.Services.AddTransient<iDataAccess, DataAccess>();  // the data access object. note: DataConfig is injected into it. 

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

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}