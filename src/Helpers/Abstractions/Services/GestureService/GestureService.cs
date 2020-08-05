using System;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service that can handle various device events.
    /// </summary>
    public partial class GestureService : IGestureService
    {
        private static IGestureService @default;

        /// <summary>
        /// It will return a single instance of <see cref="GestureService"/>,
        /// you can also set your own implemented <see cref="IGestureService"/> class.
        /// This method can hold only one instance at a time.
        /// </summary>
        public static IGestureService Default
        {
            get => @default ??= new GestureService();
            set => @default = value;
        }

        /// <summary>
        /// Raised when Backwards navigation occurs.
        /// </summary>
        public event EventHandler<GestureEventArgs> GoBackRequested;

        /// <summary>
        /// Raised when Forwards navigation occurs.
        /// </summary>
        public event EventHandler<GestureEventArgs> GoForwardRequested;

        /// <summary>
        /// Raise <see cref="GoBackRequested"/>
        /// </summary>
        /// <returns>true if handled false otherwise</returns>
        public bool RaiseGoBackRequested(object sender, GestureEventArgs args = null)
        {
            args ??= new GestureEventArgs();
            GoBackRequested?.RaiseCancelableEventReverse(sender, args);
            return args.Handled;
        }

        /// <summary>
        /// Raise <see cref="GoForwardRequested"/>
        /// </summary>
        /// <returns>true if handled false otherwise</returns>
        public bool RaiseGoForwardRequested(object sender, GestureEventArgs args = null)
        {
            args ??= new GestureEventArgs();
            GoForwardRequested?.RaiseCancelableEventReverse(sender, args);
            return args.Handled;
        }
    }
}