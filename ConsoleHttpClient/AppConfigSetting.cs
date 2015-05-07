using System;
using System.Configuration;
using JetBrains.Annotations;

namespace ConsoleHttpClientSE
{
    public static class AppConfigSetting
    {
        /// <summary>
        /// Read Configuration file App.config
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