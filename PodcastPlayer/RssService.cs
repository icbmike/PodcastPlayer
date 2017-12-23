using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace PodcastPlayer
{
    public class RssService : IRssService
    {
        public async Task<IImmutableList<ISyndicationItem>> ListFeedItems(string url)
        {
            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                var items = new List<ISyndicationItem>();
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        items.Add(await feedReader.ReadItem());
                    }
                }

                return items.ToImmutableList();
            }
        }
    }
}
