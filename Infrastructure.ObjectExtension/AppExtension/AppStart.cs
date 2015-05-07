using System.Windows.Forms;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace Infrastructure.ObjectExtension.AppExtension
{
    [UsedImplicitly]
    public static class AppStart
    {
        /// <summary>
        /// Set Startup Flag Application True
        /// </summary>
        [UsedImplicitly]
        public static void SetApplicationRun(string applicationName)
        {
            var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (!IsStartupItem(applicationName))
                // Add the value in the registry so that the application runs at startup
                if (rkApp != null)
                    rkApp.SetValue(applicationName, Application.ExecutablePath);
        }


        /// <summary>
        ///Set Startup Flag Application False 
        /// </summary>
        /// <param name="applicationName"></param>
        [UsedImplicitly]
        public static void RemoveApplicationRun(string applicationName)
        {
            //The path to the key where Windows looks for startup applications 
            var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (IsStartupItem(applicationName))

                if (rkApp != null)
                    rkApp.DeleteValue(applicationName, false);
        }

        /// <summary>
        /// The path to the key where Windows looks for startup applications
        /// </summary>
        /// <returns>bool</returns>
        private static bool IsStartupItem(string applicationName)
        {
            // The path to the key where Windows looks for startup applications
            var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            return rkApp == null || rkApp.GetValue(applicationName) != null;
        }
    }
}