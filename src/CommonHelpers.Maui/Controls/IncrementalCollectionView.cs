using System.ComponentModel;
using CommonHelpers.Collections.Interfaces;

namespace CommonHelpers.Maui.Controls
{
    public class IncrementalCollectionView : CollectionView
    {
        public IncrementalCollectionView()
        {
            PropertyChanged += IncrementalCollectionView_PropertyChanged;
        }

        private void IncrementalCollectionView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ItemsSource)) 
                return;

            if (ItemsSource is IIncrementalLoader { HasMoreItems: true } incrementalLoadingItemsSource)
            {
                incrementalLoadingItemsSource.LoadMoreItemsAsync(BatchItemCount);
            }
        }
        
        public static readonly BindableProperty BatchItemCountProperty = BindableProperty.Create(
            nameof(BatchItemCount), 
            typeof(uint), 
            typeof(IncrementalCollectionView),
            (uint)50, 
            propertyChanged: OnBatchItemCountChanged);
        
        public uint BatchItemCount
        {
            get => (uint)GetValue(BatchItemCountProperty);
            set => SetValue(BatchItemCountProperty, value);
        }

        private static void OnBatchItemCountChanged (BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is IncrementalCollectionView { ItemsSource: IIncrementalLoader { HasMoreItems: true } incrementalLoadingItemsSource })
            {
                incrementalLoadingItemsSource.LoadMoreItemsAsync((uint)newValue);
            }
        }
    }
}
