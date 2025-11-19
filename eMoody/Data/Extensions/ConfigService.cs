using Microsoft.Extensions.Configuration;

namespace eMoody.Data.Extensions
{
    public static class ConfigService
    {

        #region InjestExtraConfigs (...et al...)

        private const string configDir = "configs";
        
        public static void InjestExtraConfigs(IConfigurationBuilder builder) {
            string currDir        = AppDomain.CurrentDomain.BaseDirectory;
            string fullConfigPath = Path.Combine(currDir, configDir);

            foreach (string jsonFilename in Directory.EnumerateFiles(fullConfigPath, "*.json", SearchOption.AllDirectories)) {
                builder.AddJsonFile(jsonFilename, optional: false, reloadOnChange: true);
            }
        }


        public static void InjestExtraConfigs(this ConfigurationManager builder) 
            => InjestExtraConfigs((IConfigurationBuilder)builder);

        #endregion

    }
}