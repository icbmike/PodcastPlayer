using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PodcastPlayer.CommandRouter;
using System.Linq;

namespace PodcastPlayer.Commands
{
    public class SaveRssFeedCommand : ICommand
    {
        readonly IStore<string, Feed> _feedStore;

        public SaveRssFeedCommand(IStore<string, Feed> feedStore)
        {
            _feedStore = feedStore;
        }

        public string HelpText => "Pass a url to an rss feed. Will save it.";

        public async Task<CommandResult> ActionAsync(string commandText)
        {
            var args = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return args.Length != 2
               ? new CommandResult(true, "Incorrect number of arguments")
               : await SaveFeed(args[0], args[1]);
        }

        private async Task<CommandResult> SaveFeed(string feedName, string feedUrl)
        {
            var savedFeed = await _feedStore.SaveAsync(feedName, new Feed(){
                Name = feedName,
                Url = feedUrl
            });

            return new CommandResult(true, $"Feed '{savedFeed.Name}' saved");
        }
    }
}
