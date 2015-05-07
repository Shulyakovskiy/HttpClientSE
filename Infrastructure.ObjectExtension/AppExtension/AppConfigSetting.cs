using System;
using System.Configuration;
using JetBrains.Annotations;


namespace Infrastructure.ObjectExtension.AppExtension
{
    public static class AppConfigSetting
    {
        /// <summary>
        /// Чтение конфигурационного файла
        /// </summary>
        /// <param name="key">Setting Key</param>
        /// <returns>string</returns>
        [UsedImplicitly]
        public static string ReadSetting(string key)
        {
            var result = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                new Exception("Error reading app settings");
            }
            return result;
        }
    }
}