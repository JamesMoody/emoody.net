using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ganss.Xss;
using eMoody.Infrastructure;
using eMoody.Infrastructure.Configs;
using eMoody.DAO;
using eMoody.net.biz;

namespace eMoody.net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // config dependancy injections
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(p      => { return ServiceConfigs.Sanatizer(); });  // used in MarkdownViewer for markup/down conversion
            services.AddSingleton<DataConfig>                ((cntx) => { return ServiceConfigs.DataConfig(); }); // DataAccess' config data
            services.AddSingleton<iDataAccess, DataAccess>();                                                     // the data access object. note: DataConfig is injected into it. 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             if (env.IsDevelopment()) {
                 app.UseDeveloperExceptionPage();
             
             } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
