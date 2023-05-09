using Ganss.Xss;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eMoody.Client.BizObjects
{
    public static class ServiceConfigs
    {

        #region Config the html/markdown sanatizer

        public static HtmlSanitizer Sanatizer()
        {
            HtmlSanitizer sanitizer = new Ganss.Xss.HtmlSanitizer(); // start with the defaults
            sanitizer.AllowedTags.Add("p");                          // add <p>
            return sanitizer;
        }

        #endregion

        #region Config the httpclient to "go back to the shadow from whence you came"

        public static HttpClient PhoneHome(WebAssemblyHostBuilder builder)
        {
            return new HttpClient { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            };
        }

        #endregion

    }
}
