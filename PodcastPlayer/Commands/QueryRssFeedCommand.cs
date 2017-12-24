using PodcastPlayer.CommandRouter;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastPlayer.Commands
{
    public class QueryRssFeedCommand : ICommand
    {
        private readonly IRssService _rssService;

        public QueryRssFeedCommand(IRssService rssService)
        {
            _rssService = rssService;
        }

        public string HelpText => "Pass a url for an rss feed";

        public async Task<CommandResult> Action(string commandText)
        {
            var commandParts = commandText.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);

            return commandParts.Length == 1
                ? await FetchRssFeed(commandParts[0])
                : new CommandResult(true, "Incorrect number of arguments");
        }

        private async Task<CommandResult> FetchRssFeed(string url)
        {
            var items = await _rssService.ListFeedItems(url);

            return new CommandResult(true, string.Join("\n", items.Select(feedItem => feedItem.Title)));
        }
    }
}
