using System;

namespace Panoukos41.Helpers.Mvvm.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private string PlatformCurrentPageKey =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private string PlatformCurrentPageParameter =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private bool PlatformCanGoBack() =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private void PlatformGoBack() =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private bool PlatformCanGoForward() =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private void PlatformGoForward() =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private void PlatformNavigateTo(string pageKey, string parameter) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");
    }
}