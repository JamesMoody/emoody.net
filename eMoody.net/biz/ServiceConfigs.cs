using eMoody.Infrastructure.Configs;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eMoody.net.biz
{
    public static class ServiceConfigs
    {

        #region Config the html/markdown sanatizer

        public static HtmlSanitizer Sanatizer() {
            HtmlSanitizer sanitizer = new Ganss.XSS.HtmlSanitizer(); // start with the defaults
            sanitizer.AllowedTags.Add("p");                          // add <p>
            return sanitizer;
        }

        #endregion

        #region DataAccess' config data

        private const string format_BibleConnString = "Data Source={0};Version=3;Read Only=True;";
        private const string assetsDirectory        = "assets";          // note: moving between win & linux. Keep lowercase to avoid case sensitivity.
        private const string bibleDbFile            = "bible-sqlite.db"; //
        public static DataConfig DataConfig() {
            return new DataConfig() {
                BibleConnString = string.Format(format_BibleConnString, Path.Combine(Directory.GetCurrentDirectory(), assetsDirectory, bibleDbFile)) // note: Dev on windows, then linux + docker. Expect stupidty with path slashes & case sensitivity.
            };
        }

        #endregion

    }
}
