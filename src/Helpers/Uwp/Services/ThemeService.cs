using Windows.Storage;
using Windows.UI.Xaml;

namespace Panoukos41.Helpers.Services
{
    public partial class ThemeService : IThemeService
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private const string KEY = "OpenExtensions_User_RequestedTheme";

        /// <summary>
        /// Initializes a new instance of <see cref="ThemeService"/>
        /// </summary>
        public ThemeService()
        {
            if (!localSettings.Values.ContainsKey(KEY)
                && Window.Current.Content is FrameworkElement fe)
            {
                localSettings.Values[KEY] = (int)fe.RequestedTheme;
            }
        }

        private AppTheme PlatformGetCurrentTheme()
        {
            if (localSettings.Values.ContainsKey(KEY))
            {
                int id = (int)localSettings.Values[KEY];
                return id switch
                {
                    1 => AppTheme.Light,
                    2 => AppTheme.Dark,
                    _ => AppTheme.Default,
                };
            }
            else if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                var theme = frameworkElement.RequestedTheme switch
                {
                    ElementTheme.Light => AppTheme.Light,
                    ElementTheme.Dark => AppTheme.Dark,
                    _ => AppTheme.Default
                };
                localSettings.Values[KEY] = (int)theme;
                return theme;
            }
            else
            {
                localSettings.Values[KEY] = (int)ElementTheme.Default;
                return AppTheme.Default;
            }
        }

        private void PlatformSetTheme(AppTheme theme)
        {
            localSettings.Values[KEY] = (int)theme;
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme switch
                {
                    AppTheme.Light => ElementTheme.Light,
                    AppTheme.Dark => ElementTheme.Dark,
                    _ => ElementTheme.Default
                };
            }
        }
    }
}