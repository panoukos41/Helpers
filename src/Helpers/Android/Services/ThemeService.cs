using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;

namespace Panoukos41.Helpers.Services
{
    public partial class ThemeService : IThemeService
    {
        /// <summary>
        /// Key used to store the theme at the shared prefrences.
        /// </summary>
        protected const string KEY = "Theme";

        /// <summary>
        /// Key to get Shared prefrences.
        /// </summary>
        protected const string PREFRENCES_KEY = "com.panoukos41.helpers.android.theme";

        /// <summary>
        /// The applications shared prefrences for <see cref="PREFRENCES_KEY"/>.
        /// </summary>
        protected static ISharedPreferences SharedPreferences =>
            Application.Context.GetSharedPreferences(PREFRENCES_KEY, FileCreationMode.Private);

        /// <summary>
        /// Initializes a new <see cref="ThemeService"/> object.
        /// Don't forget to use the <see cref="FirstRun"/> before
        /// you use the Activity's SetContentView method.
        /// </summary>
        public ThemeService()
        {
        }

        /// <summary>
        /// Use before SetContentView to load the theme.
        /// </summary>
        public static void FirstRun()
        {
            if (!SharedPreferences.Contains(KEY))
            {
                var edit = SharedPreferences.Edit();
                edit.PutInt(KEY, (int)AppTheme.Default);
                edit.Apply();
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightFollowSystem;
                return;
            }

            AppCompatDelegate.DefaultNightMode = SharedPreferences.GetInt(KEY, 0) switch
            {
                (int)AppTheme.Light => AppCompatDelegate.ModeNightNo,
                (int)AppTheme.Dark => AppCompatDelegate.ModeNightYes,
                _ => AppCompatDelegate.ModeNightFollowSystem
            };
        }

        private AppTheme PlatformGetCurrentTheme() => (AppTheme)SharedPreferences.GetInt(KEY, 0);

        /// <summary>
        /// Set the provided theme.
        /// </summary>
        private void PlatformSetTheme(AppTheme theme)
        {
            AppCompatDelegate.DefaultNightMode = theme switch
            {
                AppTheme.Light => AppCompatDelegate.ModeNightNo,
                AppTheme.Dark => AppCompatDelegate.ModeNightYes,
                _ => AppCompatDelegate.ModeNightFollowSystem
            };

            var edit = SharedPreferences.Edit();
            edit.PutInt(KEY, (int)theme);
            edit.Apply();
        }
    }
}