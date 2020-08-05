using Android.App;
using Android.Content.PM;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to request permissions and determine if you already have them. <br/>
    /// You must set the Activity to use and put <see cref="OnRequestPermissionsResult(int, string[], Permission[])"/>
    /// on your activity or fragment for the service to work.<br/>
    /// This service will handle any Android permission string you pass.
    /// </summary>
    public partial class PermissionsService : IPermissionsService
    {
        private Activity _activity;
        private TaskCompletionSource<bool> TcsSinglePermission;
        private TaskCompletionSource<IDictionary<string, bool>> TcsMultiPermissions;
        private IDictionary<string, bool> permissionsResluts;

        private Activity Activity
        {
            get => _activity ?? throw new ArgumentNullException(nameof(Activity), "Activity can't be null");
            set => _activity = value;
        }

        /// <summary>
        /// The request code use by the service.
        /// </summary>
        public int RequestCode { get; private set; } = 4141;

        /// <summary>
        /// Initialize a new <see cref="PermissionsService"/>.
        /// </summary>
        public PermissionsService(int requestCode = 4141)
        {
            RequestCode = requestCode;
        }

        /// <summary>
        /// Initialize a new <see cref="PermissionsService"/> and sets
        /// the current Activity the service will use.
        /// </summary>
        public PermissionsService(Activity activity, int requestCode = 4141)
        {
            Activity = activity;
            RequestCode = requestCode;
        }

        /// <summary>
        /// Set the activity the service will use aka your main activity.
        /// </summary>
        public PermissionsService SetActivity(Activity activity)
        {
            Activity = activity;
            return this;
        }

        /// <summary>
        /// Set your own <see cref="RequestCode"/>.
        /// This code must be unique.
        /// </summary>
        public PermissionsService SetRequestCode(int requestCode)
        {
            RequestCode = requestCode;
            return this;
        }

        /// <summary>
        /// Use in the activity or fragment OnRequestPermissionsResult so the service can work.
        /// </summary>
        public void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (RequestCode != requestCode)
            {
                return;
            }
            else if (TcsSinglePermission != null)
            {
                TcsSinglePermission.SetResult(grantResults[0] == Permission.Granted);
                TcsSinglePermission = null;
            }
            else if (TcsMultiPermissions != null)
            {
                int count = permissions.Count();
                for (int i = 0; i < count; i++)
                {
                    string key = permissionsResluts.Keys.First(x => x.Equals(permissions[i]));
                    permissionsResluts[key] = grantResults[i] == Permission.Granted;
                }
                TcsMultiPermissions.SetResult(permissionsResluts);
                TcsMultiPermissions = null;
                permissionsResluts = null;
            }
        }

        /// <summary>
        /// Check if you have arleady shown the reason why you need this permission.
        /// </summary>
        public bool ShouldShowRequestPermissionRationale(string permission)
            => Activity.ShouldShowRequestPermissionRationale(permission);

        private bool CheckPermission(string permission)
            => Activity.CheckSelfPermission(permission) == Permission.Granted;

        private Task<bool> PlatformHasPermission(string permission)
            => Task.FromResult(CheckPermission(permission));

        private Task<IDictionary<string, bool>> PlatformHasPermission(params string[] permissions)
        {
            IDictionary<string, bool> dict = new Dictionary<string, bool>();
            foreach (var permission in permissions)
            {
                dict.TryAdd(permission, CheckPermission(permission));
            }
            return Task.FromResult(dict);
        }

        private Task<bool> PlatformRequestPermission(string permission)
        {
            if (CheckPermission(permission))
                return Task.FromResult(true);

            TcsSinglePermission = new TaskCompletionSource<bool>();

            Activity.RequestPermissions(new string[] { permission }, RequestCode);

            return TcsSinglePermission.Task;
        }

        private Task<IDictionary<string, bool>> PlatformRequestPermission(params string[] permissions)
        {
            if (permissions.All(CheckPermission))
            {
                IDictionary<string, bool> dict = permissions.ToDictionary(x => x, e => true);
                return Task.FromResult(dict);
            }

            TcsMultiPermissions = new TaskCompletionSource<IDictionary<string, bool>>();

            permissionsResluts = permissions.ToDictionary(x => x, e => false);

            Activity.RequestPermissions(permissions, RequestCode);

            return TcsMultiPermissions.Task;
        }
    }
}