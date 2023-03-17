using System.Threading.Tasks;

namespace CommonHelpers.Collections.Interfaces;

public interface IIncrementalLoader
{
    /// <summary>
    /// The task to be executed when loading more data
    /// </summary>
    /// <param name="count">The number of items to be fetched.</param>
    /// <returns>The number of items that were successfully fetched.</returns>
    Task<uint> LoadMoreItemsAsync(uint count);

    /// <summary>
    /// Gets a sentinel value that supports incremental loading implementations.
    /// </summary>
    /// <returns>**true** if additional unloaded items remain in the view; otherwise, **false**.</returns>
    bool HasMoreItems { get; }
}