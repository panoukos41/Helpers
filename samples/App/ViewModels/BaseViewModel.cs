using ReactiveUI;

namespace App.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; }

        public string Summary { get; protected set; }

        protected BaseViewModel() : base()
        {
            Activator = new ViewModelActivator();
        }
    }
}