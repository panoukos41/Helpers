using System.Globalization;

namespace Panoukos41.Helpers.Services
{
    public partial class LocalizationService : ILocalizationService
    {
        private void PlatformSetCulture(string cultureCode)
        {
            var cult = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = cult;
            CultureInfo.DefaultThreadCurrentUICulture = cult;
        }
    }
}