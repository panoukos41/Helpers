using Android.App;
using Android.Content;
using System.IO;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service that prompts a user to pick a file to read it or choose a place to store it. <br/>
    /// You must set the Activity to use and put <see cref="OnActivityResult(int, int, Intent)"/>
    /// on your activity for the service to work.
    /// </summary>
    public partial class FileService : IFileService
    {
        private enum Operation { Unknown = 0, Read = 1, Create = 2 }

        private Activity activity;
        private TaskCompletionSource<StreamReader> tscReader;
        private TaskCompletionSource<StreamWriter> tscWriter;
        private Operation operation = Operation.Unknown;

        /// <summary>
        /// The request code used by the service.
        /// </summary>
        public int RequestCode { get; private set; } = 4141;

        /// <summary>
        /// Initialize a new <see cref="FileService"/>.
        /// </summary>
        /// <param name="requestCode">A unique code to distinct this service.</param>
        public FileService(int requestCode = 4141)
        {
            RequestCode = requestCode;
        }

        /// <summary>
        /// Initialize a new <see cref="FileService"/> and set
        /// the current Activity the service will use.
        /// </summary>
        public FileService(Activity activity, int requestCode = 4141)
        {
            this.activity = activity;
            RequestCode = requestCode;
        }

        /// <summary>
        /// Set the activity the service will use.
        /// </summary>
        public FileService SetActivity(Activity activity)
        {
            this.activity = activity;
            return this;
        }

        /// <summary>
        /// Set your own <see cref="RequestCode"/>.
        /// This code must be unique.
        /// </summary>
        /// <param name="requestcode">A unique code to distinct this service.</param>
        public FileService SetRequestCode(int requestcode)
        {
            RequestCode = requestcode;
            return this;
        }

        /// <summary>
        /// Use at the activity or fragment OnActivityResult method for the service to work.
        /// </summary>
        public void OnActivityResult(int requestCode, Result result, Intent data)
            => OnActivityResult(requestCode, (int)result, data);

        /// <summary>
        /// Use at the activity or fragment OnActivityResult method for the service to work.
        /// </summary>
        public void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode != RequestCode) return;

            if (operation != Operation.Unknown && resultCode != (int)Result.Canceled)
            {
                if (operation == Operation.Read)
                {
                    tscReader.SetResult(new StreamReader(activity.ContentResolver.OpenInputStream(data.Data)));
                    tscReader = null;
                }
                else
                {
                    tscWriter.SetResult(new StreamWriter(activity.ContentResolver.OpenOutputStream(data.Data)));
                    tscWriter = null;
                }
                operation = Operation.Unknown;
            }
            else
            {
                if (operation == Operation.Read)
                {
                    tscReader.SetResult(null);
                    tscReader = null;
                }
                else
                {
                    tscWriter.SetResult(null);
                    tscWriter = null;
                }
            }
        }

        private Task<StreamReader> PlatformReadFileAsync(string fileType)
        {
            tscReader = new TaskCompletionSource<StreamReader>();
            operation = Operation.Read;

            var intent = new Intent(Intent.ActionOpenDocument)
                    .SetType(Intent.CategoryOpenable)
                    .SetType("*/*");
            //.SetType(fileType);

            activity.StartActivityForResult(intent, RequestCode);

            return tscReader.Task;
        }

        private Task<StreamWriter> PlatformCreateFileAsync(string fileName, string fileType)
        {
            tscWriter = new TaskCompletionSource<StreamWriter>();
            operation = Operation.Create;

            var intent = new Intent(Intent.ActionCreateDocument)
                    .SetType(Intent.CategoryOpenable)
                    .SetType("*/*")
                    .PutExtra(Intent.ExtraTitle, string.Concat(fileName, fileType));

            activity.StartActivityForResult(intent, RequestCode);

            return tscWriter.Task;
        }
    }
}