using System;

namespace Panoukos41.Helpers.Services
{
    public partial class ThemeService : IThemeService
    {
        private AppTheme PlatformGetCurrentTheme() =>
            throw new PlatformNotSupportedException(".Net Standard is not supported!");

        private void PlatformSetTheme(AppTheme theme) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported!");
    }
}