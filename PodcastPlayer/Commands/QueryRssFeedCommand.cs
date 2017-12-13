using PodcastPlayer.CommandRouter;
using System.Linq;
using System.Xml;
using Microsoft.SyndicationFeed.Rss;
using Microsoft.SyndicationFeed;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PodcastPlayer.Commands
{
    public class QueryRssFeedCommand : ICommandRoute
    {
        public string Command => "fetchRss";

        public string HelpText => "Pass a url for an rss feed";

        public CommandResult Action(string commandText)
        {
            var commandParts = commandText.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

            return commandParts.Length == 2
                ? FetchRssFeed(commandParts[1]).Result
                : new CommandResult(true, "Incorrect number of arguments");
        }

        private static async Task<CommandResult> FetchRssFeed(string url)
        {
            using (var xmlReader = XmlReader.Create(url, new XmlReaderSettings { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                var items = new List<ISyndicationItem>();
                while(await feedReader.Read())
                {
                    if(feedReader.ElementType == SyndicationElementType.Item)
                    {
                        items.Add(await feedReader.ReadItem());
                    }
                }

                return new CommandResult(true, string.Join("\n", items.Select(feedItem => feedItem.Title)));
            }
        }
    }
}
