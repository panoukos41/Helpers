using System;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// Interface for a service that can handle various device events.
    /// </summary>
    public interface IGestureService
    {
        /// <summary>
        /// Raised when Backwards navigation occurs.
        /// </summary>
        event EventHandler<GestureEventArgs> GoBackRequested;

        /// <summary>
        /// Raise the <see cref="GoBackRequested"/>
        /// </summary>
        /// <returns>True if event was handled false otherwise</returns>
        bool RaiseGoBackRequested(object sender, GestureEventArgs args = null);

        /// <summary>
        /// Raised when Forwards navigation occurs.
        /// </summary>
        event EventHandler<GestureEventArgs> GoForwardRequested;

        /// <summary>
        /// Raise the <see cref="GoForwardRequested"/>
        /// </summary>
        /// <returns>True if event was handled false otherwise</returns>
        bool RaiseGoForwardRequested(object sender, GestureEventArgs args = null);
    }
}