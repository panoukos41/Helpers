using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to request permissions and determine if you already have them.
    /// </summary>
    public partial class PermissionsService : IPermissionsService
    {
        private static IPermissionsService @default;

        /// <summary>
        /// It will return a single instance of <see cref="PermissionsService"/>,
        /// you can also set your own implemented <see cref="IPermissionsService"/> class.
        /// This method can hold only one instance at a time.
        /// </summary>
        public static IPermissionsService Default
        {
            get => @default ??= new PermissionsService();
            set => @default = value;
        }

        /// <summary>
        /// Check if you already have a permission.
        /// </summary>
        /// <returns>True if you have the permission.</returns>
        public Task<bool> HasPermission(string permission) =>
            PlatformHasPermission(permission);

        /// <summary>
        /// CHeck if you already have multiple permissions.
        /// </summary>
        /// <returns>A dictionary with permission values as keys and the result as value.
        /// True if granted permission.</returns>
        public Task<IDictionary<string, bool>> HasPermission(params string[] permissions) =>
            PlatformHasPermission(permissions);

        /// <summary>
        /// Request a permission.
        /// </summary>
        /// <returns>True if you were granted/already had permission.</returns>
        public Task<bool> RequestPermission(string permission) =>
            PlatformRequestPermission(permission);

        /// <summary>
        /// Request a list of permissions. there are static classes that provide most of the strings.
        /// Will return a dictionary in which the permission will be key and the value will
        /// be true if you were granted the permission otherwise it will be false.
        /// </summary>
        /// <returns>A dictionary with permission values as keys and the result as value.
        /// True if you were granted/already had permission.</returns>
        public Task<IDictionary<string, bool>> RequestPermission(params string[] permissions) =>
            PlatformRequestPermission(permissions);
    }
}