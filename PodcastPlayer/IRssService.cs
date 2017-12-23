using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed;

namespace PodcastPlayer
{
    public interface IRssService
    {
        Task<IImmutableList<ISyndicationItem>> ListFeedItems(string url);
    }
}
