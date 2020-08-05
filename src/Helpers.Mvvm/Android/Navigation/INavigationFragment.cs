namespace Panoukos41.Helpers.Mvvm.Navigation
{
    /// <summary>
    /// Used with <see cref="NavigationService"/> to store the provided navigation parameter.
    /// </summary>
    public interface INavigationFragment
    {
        /// <summary>
        /// A property to store the navigation parameter.
        /// </summary>
        string NavigationParameter { get; set; }
    }
}