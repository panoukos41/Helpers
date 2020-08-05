using System.Globalization;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to change culture and see current culture info.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Get the <see cref="CultureInfo.CurrentCulture"/> property.
        /// </summary>
        CultureInfo CurrentCulture { get; }

        /// <summary>
        /// Get the two letter ISO language name.
        /// </summary>
        string ISOName { get; }

        /// <summary>
        /// Set culture using the two/four letter ISO language name.
        /// </summary>
        /// <param name="cultureCode">Two or four letter ISO code.</param>
        void SetCulture(string cultureCode);
    }
}