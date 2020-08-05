using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to request permissions and determine if you already have them.
    /// </summary>
    public interface IPermissionsService
    {
        /// <summary>
        /// Check if you already have a permission.
        /// </summary>
        /// <returns>True if you have the permission.</returns>
        Task<bool> HasPermission(string permission);

        /// <summary>
        /// CHeck if you already have multiple permissions.
        /// </summary>
        /// <returns>A dictionary with permission values as keys and the result as value.
        /// True if granted permission.</returns>
        Task<IDictionary<string, bool>> HasPermission(params string[] permissions);

        /// <summary>
        /// Request a permission.
        /// </summary>
        /// <returns>True if you were granted/already had permission.</returns>
        Task<bool> RequestPermission(string permission);

        /// <summary>
        /// Request a list of permissions. there are static classes that provide most of the strings.
        /// Will return a dictionary in which the permission will be key and the value will
        /// be true if you were granted the permission otherwise it will be false.
        /// </summary>
        /// <returns>A dictionary with permission values as keys and the result as value.
        /// True if you were granted/already had permission.</returns>
        Task<IDictionary<string, bool>> RequestPermission(params string[] permissions);
    }
}