using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace App.Shell
{
    public sealed class ShellEvents : IShellEvents, IDisposable
    {
        private readonly Subject<string> whenTitleSet = new Subject<string>();

        public IObservable<string> WhenTitleSet() =>
            whenTitleSet.AsObservable();

        public void SetTitle(string title) =>
            whenTitleSet.OnNext(title);

        public void Dispose() =>
            whenTitleSet.Dispose();
    }
}