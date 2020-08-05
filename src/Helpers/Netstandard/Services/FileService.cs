using System;
using System.IO;
using System.Threading.Tasks;

namespace Panoukos41.Helpers.Services
{
    public partial class FileService
    {
        private Task<StreamReader> PlatformReadFileAsync(string fileType) =>
            throw new PlatformNotSupportedException(".Net Standard is no supported.");

        private Task<StreamWriter> PlatformCreateFileAsync(string fileName, string fileType) =>
            throw new PlatformNotSupportedException(".Net Standard is no supported.");
    }
}