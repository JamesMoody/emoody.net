using eMoody.Infrastructure.Configs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eMoody.Server.Biz
{
    public static class ConfigService
    {

        #region InjestExtraConfigs (...et al...)

        private const string configDir = "configs";
        public static void InjestExtraConfigs(IConfigurationBuilder builder) {
            var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string fullConfigPath = Path.Combine(currDir, configDir);
            foreach (var jsonFilename in Directory.EnumerateFiles(fullConfigPath, "*.json", SearchOption.AllDirectories)) {
                builder.AddJsonFile(jsonFilename, optional: false, reloadOnChange: true);
            }
        }


        public static void InjestExtraConfigs(this ConfigurationManager builder) {
            var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string fullConfigPath = Path.Combine(currDir, configDir);
            foreach (var jsonFilename in Directory.EnumerateFiles(fullConfigPath, "*.json", SearchOption.AllDirectories)) {
                builder.AddJsonFile(jsonFilename, optional: false, reloadOnChange: true);
            }
        }

        #endregion

    }
}
