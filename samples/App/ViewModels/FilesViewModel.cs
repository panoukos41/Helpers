using App.Shell;
using Panoukos41.Helpers.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class FilesViewModel : BaseViewModel
    {
        private readonly IPermissionsService permissionsService;
        private readonly IFileService fileService;

        [Reactive] public string Text { get; set; }

        public ReactiveCommand<Unit, Unit> Save { get; private set; }

        public ReactiveCommand<Unit, string> Read { get; private set; }

        public FilesViewModel(IShellEvents shellEvents, IFileService fileService, IPermissionsService permissionsService)
        {
            this.permissionsService = permissionsService;
            this.fileService = fileService;

            Save = ReactiveCommand.CreateFromTask(ExecuteSave, CanExecuteSave, RxApp.MainThreadScheduler);
            Read = ReactiveCommand.CreateFromTask(ExecuteRead, outputScheduler: RxApp.MainThreadScheduler);

            this.WhenActivated((CompositeDisposable disposable) =>
            {
                shellEvents.SetTitle("Files Example");

                Disposable
                    .Create(() =>
                    {
                        // This is called when the view model is out of view.
                    })
                    .DisposeWith(disposable);
            });

            Summary = "This sample saves or reads a string from a file using the FileService.\n" +
                "It also uses the PermissionsService to ask for read and write permissions.";
        }

        private async Task ExecuteSave()
        {
            if (!await permissionsService.RequestPermission(Permissions.WriteStorage))
                return;

            using var fileWriter = await fileService.CreateFileAsync("sample", ".json");

            if (fileWriter == null) return;

            await fileWriter.WriteAsync(Text);
        }

        private IObservable<bool> CanExecuteSave =>
            this.WhenAnyValue(x => x.Text, text => !string.IsNullOrWhiteSpace(text));

        private async Task<string> ExecuteRead()
        {
            if (!await permissionsService.RequestPermission(Permissions.ReadStorage))
                return null; // This is not really recommended

            using var fileReader = await fileService.ReadFileAsync(".json");
            return fileReader != null
                ? await fileReader.ReadToEndAsync()
                : string.Empty;
        }
    }
}