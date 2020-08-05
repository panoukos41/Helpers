using System.IO;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service that prompts a user to pick a file to read it or choose a place to create it.
    /// </summary>
    public partial class FileService : IFileService
    {
        private static IFileService @default;

        /// <summary>
        /// It will return a single instance of <see cref="FileService"/>,
        /// you can also set your own implemented <see cref="IFileService"/> class.
        /// This method can hold only one instance at a time.
        /// </summary>
        public static IFileService Default
        {
            get => @default ??= new FileService();
            set => @default = value;
        }

        /// <summary>
        /// Read a file the user picks.
        /// </summary>
        /// <returns>A <see cref="StreamReader"/> to use for reading the contents of the file.</returns>
        public Task<StreamReader> ReadFileAsync(string fileType = "*") =>
            PlatformReadFileAsync(fileType);

        /// <summary>
        /// Create a file where the user wants.
        /// </summary>
        /// <param name="fileName">The name of the file with the extension included.</param>
        /// <returns>A <see cref="StreamWriter"/> to use for writing contents to the file.</returns>
        public Task<StreamWriter> CreateFileAsync(string fileName, string fileType) =>
            PlatformCreateFileAsync(fileName, fileType);
    }
}