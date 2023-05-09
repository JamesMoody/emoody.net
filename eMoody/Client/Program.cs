using eMoody.Client;
using eMoody.Client.BizObjects;
using eMoody.Client.DAOFacade;
using eMoody.Infrastructure;
using Ganss.Xss;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace eMoody.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // services
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(p => { return ServiceConfigs.Sanatizer(); }); // used in MarkdownViewer for markup/down conversion
            builder.Services.AddTransient<HttpClient>(p => { return ServiceConfigs.PhoneHome(builder); }); // used in the dao facades to talk to the server apis. 
            builder.Services.AddSingleton<iDataAccess, DataAccess>();

            await builder.Build().RunAsync();
        }
    }
}