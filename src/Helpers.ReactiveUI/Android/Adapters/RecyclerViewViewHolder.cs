// The MIT License(MIT)
// 
// Copyright(c) .NET Foundation and Contributors
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI.AndroidX/ReactiveRecyclerViewViewHolder.cs
//
// Modified by Panagiotis Athanasiou, removed the need for the viewmodel to implement IReactiveObject.

using Android.Views;
using AndroidX.RecyclerView.Widget;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Panoukos41.Helpers.ReactiveUI.Adapters
{
    /// <summary>
    /// A <see cref="RecyclerView.ViewHolder"/> implementation that binds to a reactive view model.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public abstract class RecyclerViewViewHolder<TViewModel> : RecyclerView.ViewHolder, ILayoutViewHost, IViewFor<TViewModel>, IReactiveObject
            where TViewModel : class
    {
        /// <summary>
        /// Gets all public accessible properties.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401: Field should be private", Justification = "Legacy reasons")]
        [SuppressMessage("Design", "CA1051: Do not declare visible instance fields", Justification = "Legacy reasons")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1306: Field should start with a lower case letter", Justification = "Legacy reasons")]
        [IgnoreDataMember]
        protected Lazy<PropertyInfo[]> AllPublicProperties;

        private TViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveRecyclerViewViewHolder{TViewModel}"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        protected RecyclerViewViewHolder(View view)
            : base(view)
        {
            SetupRxObj();

            Selected = Observable.FromEvent<EventHandler, int>(
                                eventHandler =>
                                {
                                    void Handler(object sender, EventArgs e) => eventHandler(AdapterPosition);
                                    return Handler;
                                },
                                h => view.Click += h,
                                h => view.Click -= h);

            LongClicked = Observable.FromEvent<EventHandler<View.LongClickEventArgs>, int>(
                                eventHandler =>
                                {
                                    void Handler(object sender, View.LongClickEventArgs e) => eventHandler(AdapterPosition);
                                    return Handler;
                                },
                                h => view.LongClick += h,
                                h => view.LongClick -= h);

            SelectedWithViewModel = Observable.FromEvent<EventHandler, TViewModel>(
                                eventHandler =>
                                {
                                    void Handler(object sender, EventArgs e) => eventHandler(ViewModel);
                                    return Handler;
                                },
                                h => view.Click += h,
                                h => view.Click -= h);

            LongClickedWithViewModel = Observable.FromEvent<EventHandler<View.LongClickEventArgs>, TViewModel>(
                                eventHandler =>
                                {
                                    void Handler(object sender, View.LongClickEventArgs e) => eventHandler(ViewModel);
                                    return Handler;
                                },
                                h => view.LongClick += h,
                                h => view.LongClick -= h);
        }

        /// <inheritdoc/>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets an observable that signals that this ViewHolder has been selected.
        ///
        /// The <see cref="int"/> is the position of this ViewHolder in the <see cref="RecyclerView"/>
        /// and corresponds to the <see cref="RecyclerView.ViewHolder.AdapterPosition"/> property.
        /// </summary>
        public IObservable<int> Selected { get; }

        /// <summary>
        /// Gets an observable that signals that this ViewHolder has been selected.
        ///
        /// The <see cref="IObservable{TViewModel}"/> is the ViewModel of this ViewHolder in the <see cref="RecyclerView"/>.
        /// </summary>
        public IObservable<TViewModel> SelectedWithViewModel { get; }

        /// <summary>
        /// Gets an observable that signals that this ViewHolder has been long-clicked.
        ///
        /// The <see cref="int"/> is the position of this ViewHolder in the <see cref="RecyclerView"/>
        /// and corresponds to the <see cref="RecyclerView.ViewHolder.AdapterPosition"/> property.
        /// </summary>
        public IObservable<int> LongClicked { get; }

        /// <summary>
        /// Gets an observable that signals that this ViewHolder has been long-clicked.
        ///
        /// The <see cref="IObservable{TViewModel}"/> is the ViewModel of this ViewHolder in the <see cref="RecyclerView"/>.
        /// </summary>
        public IObservable<TViewModel> LongClickedWithViewModel { get; }

        /// <summary>
        /// Gets the current view being shown.
        /// </summary>
        public View View => ItemView;

        /// <inheritdoc/>
        public TViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        /// <inheritdoc/>
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel)value;
        }

        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        /// <inheritdoc/>
        void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        [OnDeserialized]
        private void SetupRxObj(StreamingContext sc)
        {
            SetupRxObj();
        }

        private void SetupRxObj()
        {
            AllPublicProperties = new Lazy<PropertyInfo[]>(() =>
                GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToArray());
        }
    }
}