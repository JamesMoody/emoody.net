using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.Config
{
    public class BibleConfig {

        public const string ConfigKey = "bibleConfig";

        public string ConnStringFormat { get; set; } = "Data Source={0};Version=3;Read Only=True;";
        public string AssetsDirectory  { get; set; } = "assets";
        public string DbFile           { get; set; } = "bible-sqlite.db";

        public string BibleConnString {
            get {
                // note: Dev on windows, then linux + docker. Expect stupidity with path slashes & case sensitivity.
                string dbFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, AssetsDirectory, DbFile); 
                return string.Format(ConnStringFormat, dbFilePath);
            }
        }
    }
}
