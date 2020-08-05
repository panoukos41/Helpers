using System.Globalization;
using Windows.Globalization;

namespace Panoukos41.Helpers.Services
{
    public partial class LocalizationService : ILocalizationService
    {
        private void PlatformSetCulture(string cultureCode)
        {
            ApplicationLanguages.PrimaryLanguageOverride = cultureCode;
            var culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}