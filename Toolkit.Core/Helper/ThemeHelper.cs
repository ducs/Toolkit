using iNKORE.UI.WPF.Helpers;
using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Toolkit;
using Windows.Storage;

namespace Toolkit.Core.Helper
{
    /// <summary>
    /// Class providing functionality around switching and restoring theme settings
    /// </summary>
    public static class ThemeHelper
    {

        static ThemeHelper()
        {
            SystemEvents_UserPreferenceChanged(null, null);
            if (Environment.OSVersion.Version.Build > 17763)
            {
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            }
        }
        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            RegistryKey registryKey = Registry.CurrentUser;
            RegistryKey personalize = registryKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            ThemeHelper.RootTheme = Convert.ToBoolean(personalize.GetValue("AppsUseLightTheme")) ? ElementTheme.Light : ElementTheme.Dark;
        }

        private const string SelectedAppThemeKey = "SelectedAppTheme";

        /// <summary>
        /// Gets the current actual theme of the app based on the requested theme of the
        /// root element, or if that value is Default, the requested theme of the Application.
        /// </summary>
        public static ElementTheme ActualTheme
        {
            get
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    if (ThemeManager.GetActualTheme(window) != ElementTheme.Default)
                    {
                        return ThemeManager.GetActualTheme(window);
                    }
                }

                  return Toolkit.Core.Services.Generic.GetEnum<ElementTheme>(ElementTheme.Default.ToString());
            }
        }

        /// <summary>
        /// Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
        /// </summary>
        public static ElementTheme RootTheme
        {
            get
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    return ThemeManager.GetActualTheme(window);
                }

                return ElementTheme.Default;
            }
            set
            {
                foreach (Window window in WindowHelper.ActiveWindows)
                {
                    ThemeManager.SetRequestedTheme(window, value);
                }

                //if (PackagedAppHelper.IsPackagedApp)
                //{
                //    ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
                //}
                //else
                //{
                //    //Properties.Settings.Default.SelectedAppTheme = value.ToString();
                //    Properties.Settings.Default.SelectedAppTheme = (int)value;
                //    Properties.Settings.Default.Save();
                //}

                UpdateSystemCaptionButtonColors();
            }
        }

        public static void Initialize()
        {
            try
            {
                //string savedTheme = PackagedAppHelper.IsPackagedApp ? ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString() : Properties.Settings.Default.SelectedAppTheme;
                string savedTheme = PackagedAppHelper.IsPackagedApp ? ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString() : ElementTheme.Light.ToString();//((ElementTheme)Properties.Settings.Default.SelectedAppTheme).ToString();

                if (savedTheme != null)
                {
                    RootTheme = Services.Generic.GetEnum<ElementTheme>(savedTheme);
                }
            }
            catch { }
        }

        public static bool IsDarkTheme()
        {
            if (RootTheme == ElementTheme.Default)
            {
                return ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark;
            }
            return RootTheme == ElementTheme.Dark;
        }

        public static void UpdateSystemCaptionButtonColors()
        {
        }
    }
}
