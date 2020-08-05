using System;

namespace App.Shell
{
    public interface IShellEvents
    {
        IObservable<string> WhenTitleSet();

        void SetTitle(string title);
    }
}