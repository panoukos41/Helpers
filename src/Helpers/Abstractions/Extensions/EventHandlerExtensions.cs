using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to be used with <see cref="EventHandler{TEventArgs}"/> classes.
    /// </summary>
    public static partial class EventHandlerExtensions
    {
        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in order.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void RaiseEvent<T>(this EventHandler<T> eventHandler, object sender, T args)
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList();

                foreach (EventHandler<T> del in invocationList)
                {
                    del(sender, args);
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in order and stops if a canceled event is found.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void RaiseCancelableEvent<T>(this EventHandler<T> eventHandler, object sender, T args) where T : CancelEventArgs
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList();

                foreach (EventHandler<T> del in invocationList)
                {
                    del(sender, args);

                    if (args.Cancel)
                        break;
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in reverse order.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void RaiseEventReverse<T>(this EventHandler<T> eventHandler, object sender, T args)
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList().Reverse();

                foreach (EventHandler<T> del in invocationList)
                {
                    del(sender, args);
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in reverse order and stops if a canceled event is found.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void RaiseCancelableEventReverse<T>(this EventHandler<T> eventHandler, object sender, T args) where T : CancelEventArgs
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList().Reverse();

                foreach (EventHandler<T> del in invocationList)
                {
                    del(sender, args);

                    if (args.Cancel)
                        break;
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in order.
        /// If an event throws an exception it will be ignored.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void TryRaiseEvent<T>(this EventHandler<T> eventHandler, object sender, T args)
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList();

                foreach (EventHandler<T> del in invocationList)
                {
                    try
                    {
                        del(sender, args);
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch { } // Events should be fire and forget, subscriber fail should not affect publishing process
#pragma warning restore CA1031 // Do not catch general exception types
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in order and stops if a canceled event is found.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void TryRaiseCancelableEvent<T>(this EventHandler<T> eventHandler, object sender, T args) where T : CancelEventArgs
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList();

                foreach (EventHandler<T> del in invocationList)
                {
                    try
                    {
                        del(sender, args);

                        if (args.Cancel)
                            break;
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch { } // Events should be fire and forget, subscriber fail should not affect publishing process
#pragma warning restore CA1031 // Do not catch general exception types
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in reverse order.
        /// If an event throws an exception it will be ignored.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void TryRaiseEventReverse<T>(this EventHandler<T> eventHandler, object sender, T args)
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList().Reverse();

                foreach (EventHandler<T> del in invocationList)
                {
                    try
                    {
                        del(sender, args);
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch { } // Events should be fire and forget, subscriber fail should not affect publishing process
#pragma warning restore CA1031 // Do not catch general exception types
                }
            }
        }

        /// <summary>
        /// Invokes all the handlers attached to an eventhandler in reverse order and stops if a canceled event is found.
        /// If an event throws an exception it will be ignored.
        /// </summary>
        /// <typeparam name="T">The type of the eventArgs.</typeparam>
        /// <param name="eventHandler">The EventHandler.</param>
        /// <param name="sender">The object that sends this event.</param>
        /// <param name="args">The eventArgs.</param>
        public static void TryRaiseCancelableEvenReverset<T>(this EventHandler<T> eventHandler, object sender, T args) where T : CancelEventArgs
        {
            EventHandler<T> handler = eventHandler;

            if (handler != null)
            {
                IEnumerable<Delegate> invocationList = handler.GetInvocationList().Reverse();

                foreach (EventHandler<T> del in invocationList)
                {
                    try
                    {
                        del(sender, args);

                        if (args.Cancel)
                            break;
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch { } // Events should be fire and forget, subscriber fail should not affect publishing process
#pragma warning restore CA1031 // Do not catch general exception types
                }
            }
        }
    }
}