using System;
using System.Linq;
using Windows.UI.Xaml;
using CommonHelpers.Collections;
using CommonHelpers.Common;
using CommonHelpers.Models;

namespace Demo.Uwp.ViewModels
{
    public class CollectionsViewModel : ViewModelBase
    {
        private Random _random;
        private int _totalItemsAdded;

        public CollectionsViewModel()
        {
            _random = new Random();
            _totalItemsAdded = 5;

            var initialItems = Enumerable.Range(1, _totalItemsAdded).Select(i => $"Item {i}").ToList();

            foreach (var item in initialItems)
            {
                // queue the initial items
                Queue.Enqueue(item);

                var itemNumber = initialItems.IndexOf(item) + 1;

                People.Add(new Person
                {
                    Name = $"Person {itemNumber}",
                    Age = itemNumber
                });
            }
        }

        public ObservableQueue<string> Queue { get; } = new ObservableQueue<string>();

        public ObservableRangeCollection<Person> People { get; } = new ObservableRangeCollection<Person>();
        
        public void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Queue an item
            Queue.Enqueue($"Item {Queue.Count + 1}");

            // Add a range (5 items)
            People.AddRange(Enumerable.Range(_totalItemsAdded + 1, 5).Select(i => new Person
            {
                Name = $"Person {i + 1}",
                Age = i + 1
            }));

            // keep track of how many were added so we can compare what items were removed.
            _totalItemsAdded = People.Count;
        }

        public void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Dequeue - removes the last added item (LIFO)
            Queue.Dequeue();
            
            // Remove the first and last item if there are enough items
            if(People.Count > 2)
            {
                People.RemoveRange(new[] {People.FirstOrDefault(), People.LastOrDefault()});
            }
        }
    }
}
