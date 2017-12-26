using System.Collections.Immutable;
using System.Threading.Tasks;

namespace PodcastPlayer
{
    public interface IStore<TKey, T>
    {
        Task<T> SaveAsync(TKey key, T item);

        Task<T> GetAsync(TKey key);

        Task<IImmutableList<T>> ListAsync();
    }
}