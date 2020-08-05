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
// https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI.AndroidX/ReactiveRecyclerViewAdapter.cs
//
// Modified by Panagiotis Athanasiou, removed the need for the viewmodel to implement IReactiveObject.

using AndroidX.RecyclerView.Widget;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Panoukos41.Helpers.ReactiveUI.Adapters
{
    // todo add reactiveui lisence
    /// <summary>
    /// An adapter for the Android <see cref="RecyclerView"/>.
    /// </summary>
    /// <typeparam name="TViewModel">The type of ViewModel that this adapter holds.</typeparam>
    public abstract class RecyclerViewAdapter<TViewModel> : RecyclerView.Adapter
        where TViewModel : class
    {
        private readonly ISourceList<TViewModel> _list;

        private readonly IDisposable _inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecyclerViewAdapter{TViewModel}"/> class.
        /// </summary>
        /// <param name="backingList">The backing list.</param>
        protected RecyclerViewAdapter(IEnumerable<TViewModel> backingList)
        {
            _list = new SourceList<TViewModel>();
            _list.AddRange(backingList);
            _inner = _list
                .Connect()
                .Subscribe();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecyclerViewAdapter{TViewModel}"/> class.
        /// </summary>
        /// <param name="backingList">The backing list.</param>
        protected RecyclerViewAdapter(ObservableCollection<TViewModel> backingList) : this(backingList.ToObservableChangeSet())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecyclerViewAdapter{TViewModel}"/> class.
        /// </summary>
        /// <param name="backingList">The backing list.</param>
        protected RecyclerViewAdapter(IObservable<IChangeSet<TViewModel>> backingList)
        {
            _list = new SourceList<TViewModel>(backingList);
            _inner = _list
                .Connect()
                .ForEachChange(UpdateBindings)
                .Subscribe();
        }

        /// <inheritdoc/>
        public override int ItemCount => _list.Count;

        /// <inheritdoc/>
        public override int GetItemViewType(int position) =>
            GetItemViewType(position, GetViewModelByPosition(position));

        /// <summary>
        /// Determine the View that will be used/re-used in lists where
        /// the list contains different cell designs.
        /// </summary>
        /// <param name="position">The position of the current view in the list.</param>
        /// <param name="viewModel">The ViewModel associated with the current View.</param>
        /// <returns>An ID to be used in OnCreateViewHolder.</returns>
        public virtual int GetItemViewType(int position, TViewModel viewModel) =>
            base.GetItemViewType(position);

        /// <inheritdoc/>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder == null)
            {
                throw new ArgumentNullException(nameof(holder));
            }

            if (!(holder is IViewFor viewForHolder))
            {
                throw new ArgumentException("Holder must be derived from IViewFor", nameof(holder));
            }
            viewForHolder.ViewModel = GetViewModelByPosition(position);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _inner?.Dispose();
                _list?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected TViewModel GetViewModelByPosition(int position)
        {
            return position >= _list.Count ? null : _list.Items.ElementAt(position);
        }

        private void UpdateBindings(Change<TViewModel> change)
        {
            switch (change.Reason)
            {
                case ListChangeReason.Add:
                    NotifyItemInserted(change.Item.CurrentIndex);
                    break;

                case ListChangeReason.Remove:
                    NotifyItemRemoved(change.Item.CurrentIndex);
                    break;

                case ListChangeReason.Moved:
                    NotifyItemMoved(change.Item.PreviousIndex, change.Item.CurrentIndex);
                    break;

                case ListChangeReason.Replace:
                case ListChangeReason.Refresh:
                    NotifyItemChanged(change.Item.CurrentIndex);
                    break;

                case ListChangeReason.AddRange:
                    NotifyItemRangeInserted(change.Range.Index, change.Range.Count);
                    break;

                case ListChangeReason.RemoveRange:
                case ListChangeReason.Clear:
                    NotifyItemRangeRemoved(change.Range.Index, change.Range.Count);
                    break;
            }
        }
    }
}