using System.Globalization;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to change culture and see current culture info.
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        private static ILocalizationService @default;

        /// <summary>
        /// It will return a single instance of <see cref="LocalizationService"/>,
        /// you can also set your own implemented <see cref="ILocalizationService"/> class.
        /// This method can hold only one instance at a time.
        /// </summary>
        public static ILocalizationService Default
        {
            get => @default ??= new LocalizationService();
            set => @default = value;
        }

        /// <summary>
        /// Get the <see cref="CultureInfo.CurrentCulture"/> property.
        /// </summary>
        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        /// <summary>
        /// Get the two letter ISO language name.
        /// </summary>
        public string ISOName => CurrentCulture.TwoLetterISOLanguageName;

        /// <summary>
        /// Set culture using the two/four letter ISO language name.
        /// </summary>
        /// <param name="cultureCode">Two or four letter ISO code.</param>
        public void SetCulture(string cultureCode) =>
            PlatformSetCulture(cultureCode);
    }
}