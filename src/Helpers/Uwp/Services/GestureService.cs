// Copyright 2015 Jerry Nixon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Modified by Athanasiou Panagiotis, modifications removed some windows phone code and moved some code to extension methods.
//
// This is the original ModalDialog of template 10 since the project is
// not actively maintained or the control is not ported forward.
// https://github.com/Windows-XAML/Template10
//

using System;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Panoukos41.Helpers.Services
{
    public partial class GestureService : IGestureService, IDisposable
    {
        private static bool? isKeyboardPresent;
        private static bool? isMousePresent;
        private static bool? isTouchPresent;

        /// <summary>
        /// True if a keyboards is present.
        /// /summary>
        public static bool IsKeyboardPresent => isKeyboardPresent ??= new KeyboardCapabilities().KeyboardPresent != 0;

        /// <summary>
        /// True if a mouse is present.
        /// </summary>
        public static bool IsMousePresent => isMousePresent ??= new MouseCapabilities().MousePresent != 0;

        /// <summary>
        /// True if touchscreen is present.
        /// </summary>
        public static bool IsTouchPresent => isTouchPresent ??= new TouchCapabilities().TouchPresent != 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="GestureService"/>
        /// </summary>
        public GestureService()
        {
            if (IsMousePresent)
                MouseDevice.GetForCurrentView().MouseMoved += OnMouseMoved;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnSystemNavigationManagerBackRequested;
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += OnAcceleratorKeyActivated;
            Window.Current.CoreWindow.PointerPressed += OnPointerPressed;
        }

        /// <summary>
        /// Event fired when the mouse is moved.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMoved;

        /// <summary>
        /// Event fired when an accelarator key is activated (pressed or held down).
        /// </summary>
        public event TypedEventHandler<CoreDispatcher, AcceleratorKeyEventArgs> AcceleratorKeyActivated;

        /// <summary>
        /// </summary>
        public event TypedEventHandler<CoreWindow, PointerEventArgs> PointerPressed;

        /// <summary>
        /// Dispose implementation - unsubscribes from nav state changes.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (IsMousePresent)
                    MouseDevice.GetForCurrentView().MouseMoved -= OnMouseMoved;

                SystemNavigationManager.GetForCurrentView().BackRequested -= OnSystemNavigationManagerBackRequested;
                Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -= OnAcceleratorKeyActivated;
                Window.Current.CoreWindow.PointerPressed -= OnPointerPressed;
            }
        }

        /// <summary>
        /// Handle <see cref="MouseMoved"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseMoved(MouseDevice sender, MouseEventArgs args)
        {
            MouseMoved?.Invoke(sender, args);
        }

        /// <summary>
        /// Handle <see cref="SystemNavigationManager.BackRequested"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = RaiseGoBackRequested(sender);
        }

        /// <summary>
        /// Handle <see cref="AcceleratorKeyActivated"/> event.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        protected virtual void OnAcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            AcceleratorKeyActivated?.Invoke(sender, args);
        }

        /// <summary>
        /// Handle every mouse click, touch screen tap, or equivalent interaction.
        /// Used to detect browser-style next and previous mouse button clicks to navigate between pages.
        /// By defualt the 2 mouse keys work like the broswer, ovveride to implement your own logic.
        /// Make sure you are not handling the event (Window.Current.CoreWindow.PointerPressed) yourself for the service to function correctly.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        protected virtual void OnPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            args.Handled = false; // I noticed handled always comes true for some reason, so since we are the first we set it to false.
            PointerPressed?.Invoke(sender, args);
            if (args.Handled) return;

            var properties = args.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed)
                return;

            // If back or foward are pressed (but not both) navigate appropriately
            bool backPressed = properties.IsXButton1Pressed;
            bool forwardPressed = properties.IsXButton2Pressed;

            if (backPressed ^ forwardPressed)
            {
                args.Handled = true;

                if (backPressed)
                    RaiseGoBackRequested(sender);

                if (forwardPressed)
                    RaiseGoForwardRequested(sender);
            }
        }
    }
}