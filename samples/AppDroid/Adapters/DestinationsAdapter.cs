using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Panoukos41.Helpers;
using Panoukos41.Helpers.ReactiveUI.Adapters;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AppDroid.Adapters
{
    public class DestinationsAdapter : RecyclerViewAdapter<string>
    {
        private readonly Subject<string> whenItemSelected = new Subject<string>();

        public DestinationsAdapter(IEnumerable<string> backingList) : base(backingList)
        {
        }

        public DestinationsAdapter(ObservableCollection<string> backingList) : base(backingList)
        {
        }

        public IObservable<string> WhenItemSelected =>
            whenItemSelected.AsObservable();

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var destinationViewHolder = new DestinationViewHolder(parent.Inflate(Resource.Layout.adapter_destination));
            destinationViewHolder.SelectedWithViewModel.Subscribe(x => whenItemSelected.OnNext(x));
            return destinationViewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            var destinationViewHolder = holder as DestinationViewHolder;
            destinationViewHolder.Button.Text = destinationViewHolder.ViewModel;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;

            whenItemSelected.OnCompleted();
            whenItemSelected.Dispose();
            base.Dispose(disposing);
        }

        private class DestinationViewHolder : RecyclerViewViewHolder<string>
        {
            public Button Button { get; set; }

            public DestinationViewHolder(View view) : base(view)
            {
                ControlFetcherMixin.WireUpControls(this);
            }
        }
    }
}