using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace Panoukos41.Helpers.Services
{
    public partial class FileService : IFileService
    {
        private async Task<StreamReader> PlatformReadFileAsync(string fileType)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // File types the user can pick.
            openPicker.FileTypeFilter.Add(fileType);

            var file = await openPicker.PickSingleFileAsync();

            return file != null
                ? new StreamReader(await file.OpenStreamForReadAsync())
                : null;
        }

        private async Task<StreamWriter> PlatformCreateFileAsync(string fileName, string fileType)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Dropdown of file types the user can save the file as.
            savePicker.FileTypeChoices.Add("File", new List<string>() { fileType });
            // Default file name if the user does not type one in or select a file to replace.
            savePicker.SuggestedFileName = fileName;

            var file = await savePicker.PickSaveFileAsync();

            return file != null
                ? new StreamWriter(await file.OpenStreamForWriteAsync())
                : null;
        }
    }
}