using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CommonHelpers.Collections;

/// <summary> 
/// Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed. 
/// </summary> 
/// <typeparam name="T"></typeparam> 
public class ObservableRangeCollection<T> : ObservableCollection<T>
{
    private NotifyCollectionChangedAction defaultAddCollectionChangedAction;
    private NotifyCollectionChangedAction defaultRemoveCollectionChangedAction;

    /// <summary>
    /// States the maximum number of items the collection can hold. IF the count is larger than this value, the first item will be removed.
    /// </summary>
    public int? MaximumCount { get; set; } = null;

    /// <inheritdoc />
    /// <summary>
    /// Override OnCollectionChanged with logic that removes the oldest item is the count is larger than the MaximumCount. This will repeat until the count is less than or equal to the MaximumCount.
    /// </summary> 
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnCollectionChanged(e);

        if (MaximumCount != null && this.Count > MaximumCount)
        {
            base.RemoveAt(0);
        }
    }

    /// <inheritdoc />
    /// <summary> 
    /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class. 
    /// <param name="defaultAddAction">Sets the default mode for the AddRange collection changed notification (default is NotifyCollectionChangedAction.Add).</param>
    /// <param name="defaultRemoveAction">Sets the default mode for the RemoveRange collection changed notification (default is NotifyCollectionChangedAction.Reset).</param>
    /// </summary> 
    public ObservableRangeCollection(
        NotifyCollectionChangedAction defaultAddAction = NotifyCollectionChangedAction.Add, 
        NotifyCollectionChangedAction defaultRemoveAction = NotifyCollectionChangedAction.Reset) : base()
    {
        defaultAddCollectionChangedAction = defaultAddAction;
        defaultRemoveCollectionChangedAction = defaultRemoveAction;
    }

    /// <inheritdoc />
    /// <summary> 
    /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class that contains elements copied from the specified collection. 
    /// </summary> 
    /// <param name="collection">The collection from which the elements are copied.</param>
    /// <param name="defaultAddAction">Sets the default mode for the AddRange collection changed notification (default is NotifyCollectionChangedAction.Add).</param>
    /// <param name="defaultRemoveAction">Sets the default mode for the RemoveRange collection changed notification (default is NotifyCollectionChangedAction.Reset).</param>
    /// <exception cref="T:System.ArgumentNullException">The collection parameter cannot be null.</exception> 
    public ObservableRangeCollection(
        IEnumerable<T> collection, 
        NotifyCollectionChangedAction defaultAddAction = NotifyCollectionChangedAction.Add, 
        NotifyCollectionChangedAction defaultRemoveAction = NotifyCollectionChangedAction.Reset) : base(collection)
    {
        defaultAddCollectionChangedAction = defaultAddAction;
        defaultRemoveCollectionChangedAction = defaultRemoveAction;
    }

    public NotifyCollectionChangedAction DefaultAddCollectionChangedAction
    {
        get => defaultAddCollectionChangedAction;
        set
        {
            if (value == defaultAddCollectionChangedAction)
                return;

            defaultAddCollectionChangedAction = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(DefaultAddCollectionChangedAction)));
        }
    }

    public NotifyCollectionChangedAction DefaultRemoveCollectionChangedAction
    {
        get => defaultRemoveCollectionChangedAction;
        set
        {
            if (value == defaultRemoveCollectionChangedAction)
                return;

            defaultRemoveCollectionChangedAction = value;
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(DefaultRemoveCollectionChangedAction)));
        }
    }

    /// <summary> 
    /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
    /// </summary> 
    public void AddRange(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        CheckReentrancy();

        if (DefaultAddCollectionChangedAction == NotifyCollectionChangedAction.Reset)
        {
            foreach (var i in collection)
                Items.Add(i);

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(DefaultAddCollectionChangedAction));
        }
        else
        {
            var startIndex = Count;

            var changedItems = collection as List<T> ?? new List<T>(collection);

            foreach (var i in changedItems)
            {
                Items.Add(i);
            }
                
            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(DefaultAddCollectionChangedAction, changedItems, startIndex));
        }
    }

    /// <summary> 
    /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
    /// </summary> 
    public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode)
    {
        if (notificationMode != NotifyCollectionChangedAction.Add && 
            notificationMode != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Add or Reset for AddRange.", nameof(notificationMode));

        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        CheckReentrancy();

        if (notificationMode == NotifyCollectionChangedAction.Reset)
        {
            foreach (var i in collection)
            {
                Items.Add(i);
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(notificationMode));
        }
        else
        {
            var startIndex = Count;

            var changedItems = collection as List<T> ?? new List<T>(collection);

            foreach (var i in changedItems)
            {
                Items.Add(i);
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(notificationMode, changedItems, startIndex));
        }
    }

    /// <summary> 
    /// Removes the first occurrence of each item in the specified collection from ObservableCollection(Of T).
    /// NOTE: with notificationMode = Remove, removed items starting index is not set because items are not guaranteed to be consecutive.
    /// </summary> 
    public void RemoveRange(IEnumerable<T> collection)
    {
        if (DefaultRemoveCollectionChangedAction != NotifyCollectionChangedAction.Remove &&
            DefaultRemoveCollectionChangedAction != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Remove or Reset for RemoveRange.", nameof(DefaultRemoveCollectionChangedAction));

        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        CheckReentrancy();

        if (DefaultRemoveCollectionChangedAction == NotifyCollectionChangedAction.Reset)
        {

            foreach (var i in collection)
            {
                Items.Remove(i);
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(DefaultRemoveCollectionChangedAction));
        }
        else
        {
            var changedItems = collection as List<T> ?? new List<T>(collection);

            for (var i = 0; i < changedItems.Count; i++)
            {
                if (Items.Remove(changedItems[i])) 
                    continue;

                changedItems.RemoveAt(i); //Can't use a foreach because changedItems is intended to be (carefully) modified

                i--;
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(DefaultRemoveCollectionChangedAction, changedItems, -1));
        }
    }

    /// <summary> 
    /// Removes the first occurrence of each item in the specified collection from ObservableCollection(Of T).
    /// NOTE: with notificationMode = Remove, removed items starting index is not set because items are not guaranteed to be consecutive.
    /// </summary> 
    public void RemoveRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode)
    {
        if (notificationMode != NotifyCollectionChangedAction.Remove &&
            notificationMode != NotifyCollectionChangedAction.Reset)
            throw new ArgumentException("Mode must be either Remove or Reset for RemoveRange.", nameof(notificationMode));

        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        CheckReentrancy();

        if (notificationMode == NotifyCollectionChangedAction.Reset)
        {

            foreach (var i in collection)
            {
                Items.Remove(i);
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(notificationMode));
        }
        else
        {
            var changedItems = collection as List<T> ?? new List<T>(collection);

            for (var i = 0; i < changedItems.Count; i++)
            {
                if (Items.Remove(changedItems[i])) 
                    continue;

                changedItems.RemoveAt(i); //Can't use a foreach because changedItems is intended to be (carefully) modified

                i--;
            }

            RaisePropertyChangeNotifications();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(notificationMode, changedItems, -1));
        }
    }

    /// <summary> 
    /// Clears the current collection and replaces it with the specified item. 
    /// </summary> 
    public void Replace(T item) => ReplaceRange(new T[] { item });

    /// <summary> 
    /// Clears the current collection and replaces it with the specified collection. 
    /// </summary> 
    public void ReplaceRange(IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        Items.Clear();

        AddRange(collection, this.DefaultAddCollectionChangedAction);
    }

    // Shared method
    private void RaisePropertyChangeNotifications()
    {
        OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
    }
}