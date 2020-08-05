namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to set the device theme, and determine which theme is active.
    /// Before using the service please follow the implemented object xml documentation.
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// Get the current theme.
        /// </summary>
        AppTheme GetCurrentTheme();

        /// <summary>
        /// Set the provided theme.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        void SetTheme(AppTheme theme);
    }
}