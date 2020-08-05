using System.IO;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service that prompts a user to pick a file to read it or choose a place to create it.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Read a file the user picks.
        /// </summary>
        /// <returns>A <see cref="StreamReader"/> to use for reading the contents of the file.</returns>
        Task<StreamReader> ReadFileAsync(string fileType = "*");

        /// <summary>
        /// Create a file where the user wants.
        /// </summary>
        /// <param name="fileName">The name of the file with the extension included.</param>
        /// <returns>A <see cref="StreamWriter"/> to use for writing contents to the file.</returns>
        Task<StreamWriter> CreateFileAsync(string fileName, string fileType);
    }
}