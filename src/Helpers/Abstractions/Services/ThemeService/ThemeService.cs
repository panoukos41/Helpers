namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service that helps you change themes that implements <see cref="IThemeService"/>.
    /// </summary>
    public partial class ThemeService : IThemeService
    {
        private static IThemeService @default;

        /// <summary>
        /// It will return a single instance of <see cref="ThemeService"/>,
        /// you can also set your own implemented <see cref="IThemeService"/> class.
        /// This method can hold only one instance at a time.
        /// </summary>
        public static IThemeService Default
        {
            get => @default ??= new ThemeService();
            set => @default = value;
        }

        /// <summary>
        /// Get the current theme.
        /// </summary>
        /// <returns></returns>
        public virtual AppTheme GetCurrentTheme() =>
            PlatformGetCurrentTheme();

        /// <summary>
        /// Set the provided theme.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        public virtual void SetTheme(AppTheme theme) =>
            PlatformSetTheme(theme);
    }
}