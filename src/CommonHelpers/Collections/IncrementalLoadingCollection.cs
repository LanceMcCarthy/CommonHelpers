using CommonHelpers.Collections.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelpers.Collections
{
    public class IncrementalLoadingCollection<T> : ObservableCollection<T>, IIncrementalLoader
    {
        private readonly Func<CancellationToken, uint, Task<ObservableCollection<T>>> _func;
        private uint _maxItems;
        private bool _isInfinite;
        private CancellationToken _token;

        /// <summary>
        /// Incremental scrolling list supported by UI controls that implement ISupportIncrementalLoading
        /// </summary>
        /// <param name="func"></param>
        public IncrementalLoadingCollection(Func<CancellationToken, uint, Task<ObservableCollection<T>>> func)
            : this(func, 0) { }

        /// <summary>
        /// Incremental scrolling list supported by UI controls that implement ISupportIncrementalLoading
        /// </summary>
        /// <param name="func">Task that retrieves the items</param>
        /// <param name="maximumItems">Set to the maximum number of items to expect</param>
        public IncrementalLoadingCollection(Func<CancellationToken, uint, Task<ObservableCollection<T>>> func, uint maximumItems)
        {
            _func = func;

            if (maximumItems == 0) //Infinite
            {
                _isInfinite = true;
            }
            else
            {
                _maxItems = maximumItems;

                _isInfinite = false;
            }
        }

        public bool HasMoreItems
        {
            get
            {
                if (_token.IsCancellationRequested) 
                    return false;

                if (_isInfinite) 
                    return true;

                return this.Count < _maxItems;
            }
        }

        public Task<uint> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(() => this.InternalLoadMoreItemsAsync(_token, count), _token);
        }

        private async Task<uint> InternalLoadMoreItemsAsync(CancellationToken passedToken, uint count)
        {
            _token = passedToken;
            var baseIndex = this.Count;
            uint numberOfItemsToGet = 0;

            if (!_isInfinite)
            {
                if (baseIndex + count < _maxItems)
                {
                    numberOfItemsToGet = count;
                }
                else
                {
                    numberOfItemsToGet = _maxItems - (uint) (baseIndex);
                }
            }
            else
            {
                numberOfItemsToGet = count;
            }

            var tempList = await _func(passedToken, numberOfItemsToGet);

            if (tempList.Count == 0) // stop incremental loading 
            {
                _maxItems = (uint)this.Count;
                _isInfinite = false;
            }
            else
            {
                foreach (var item in tempList)
                {
                    this.Add(item);
                }
            }

            return (uint)tempList.Count;
        }
    }
}
