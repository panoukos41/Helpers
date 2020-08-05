using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    public partial class PermissionsService : IPermissionsService
    {
        private Task<bool> PlatformHasPermission(string permission) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private Task<IDictionary<string, bool>> PlatformHasPermission(params string[] permissions) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private Task<bool> PlatformRequestPermission(string permission) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");

        private Task<IDictionary<string, bool>> PlatformRequestPermission(params string[] permissions) =>
            throw new PlatformNotSupportedException(".Net Standard is not supported.");
    }
}