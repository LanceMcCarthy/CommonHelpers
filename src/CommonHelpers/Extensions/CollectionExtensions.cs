using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CommonHelpers.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Adds a range of items to a collection.
    /// -- IMPORTANT: This does not suspend CollectionChanged notifications. If you require a single CollectionChanged notification, use ObservableRangeCollection instead.
    /// </summary>
    /// <param name="target">The target collection where the <paramref name="source" /> items are going to be added to.</param>
    /// <param name="source">The items to be added to <paramref name="target" />.</param>
    /// <exception cref="ArgumentNullException">Both the source and target collections must not be null.</exception>
    public static void AddRange<T>(this ObservableCollection<T> target, IEnumerable<T> source)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        if (source == null)
            throw new ArgumentNullException(nameof(source));

        foreach (var i in source)
            target.Add(i);
    }

    /// <summary>
    /// Inserts a range of items into a collection, starting at a desired index.
    /// -- IMPORTANT: This does not suspend CollectionChanged notifications. If you require a single CollectionChanged notification, use ObservableRangeCollection instead.
    /// </summary>
    /// <param name="target">The target collection where the <paramref name="source" /> items are going to be inserted to.</param>
    /// <param name="source">The items to be inserted into <paramref name="target" /> </param>
    /// <param name="startIndex">The zero-based index at which <paramref name="source" /> should start the insertion.</param>
    /// <exception cref="ArgumentNullException">Both the source and target collections must not be null.</exception>
    /// /// <exception cref="ArgumentOutOfRangeException">The value for <paramref name="startIndex" /> cannot be less than zero.</exception>
    public static void InsertRange<T>(this ObservableCollection<T> target, IEnumerable<T> source, int startIndex)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (startIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(startIndex));

        var sourceItems = source.ToArray();

        for (var i = 0; i < sourceItems.Length; i++)
        {
            target.Insert(startIndex + i, sourceItems[i]);
        }
    }

    /// <summary>
    /// Removes a range of items from a collection, starting at a desired index. 
    /// -- IMPORTANT: This does not suspend CollectionChanged notifications. If you require a single CollectionChanged notification, use ObservableRangeCollection instead.
    /// </summary>
    /// <param name="target">The target collection from which the items will be removed</param>
    /// <param name="startIndex">The starting index to remove items from.</param>
    /// <param name="count">Number of items to be removed</param>
    /// <exception cref="ArgumentNullException">Both the source and target collections must not be null.</exception>
    /// /// <exception cref="ArgumentOutOfRangeException">The value for <paramref name="startIndex" /> cannot be less than zero.</exception>
    public static void RemoveRange<T>(this ObservableCollection<T> target, int startIndex, int count)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        if (startIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        var itemsToRemove = new List<T>();

        for (var i = 0; i < target.Count - 1; i++)
        {
            if(i >= startIndex && i < startIndex + count)
                itemsToRemove.Add(target[i]);
        }

        foreach (var o in itemsToRemove)
        {
            target.Remove(o);
        }
    }
}
