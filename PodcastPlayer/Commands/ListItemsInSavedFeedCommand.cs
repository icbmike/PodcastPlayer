using System;
using System.Linq;
using System.Threading.Tasks;
using PodcastPlayer.CommandRouter;

namespace PodcastPlayer.Commands
{
    public class ListItemsInSavedFeedCommand : ICommand
    {
        private readonly IRssService _rssService;
        readonly IStore<string, Feed> _feedStore;

        public ListItemsInSavedFeedCommand(IRssService rssService, IStore<string, Feed> feedStore)
        {
            _feedStore = feedStore;
            _rssService = rssService;
        }

        public string HelpText => "Lists items in a saved feed";

        public async Task<CommandResult> ActionAsync(string commandText)
        {
            var commandParts = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return commandParts.Length != 1
               ? new CommandResult(true, "Incorrect number of arguments.")
               : await ListItems(commandParts[0]);
        }

        private async Task<CommandResult> ListItems(string feedName)
        {
            var feed = await _feedStore.GetAsync(feedName);

            var feedItems = await _rssService.ListFeedItems(feed.Url);

            var message = string.Join("\n", feedItems.Select(feedItem => feedItem.Title));

            return new CommandResult(true, message);
        }
    }
}
