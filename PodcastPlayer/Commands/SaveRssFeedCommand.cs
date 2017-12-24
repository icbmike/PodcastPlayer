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
        public string HelpText => "Pass a url to an rss feed. Will save it.";

        public async Task<CommandResult> Action(string commandText)
        {
            var args = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return args.Length != 2
               ? new CommandResult(true, "Incorrect number of arguments")
               : await SaveFeed(args[0], args[1]);
        }

        private async Task<CommandResult> SaveFeed(string feedName, string feedUrl)
        {
            await FileExtensions.CreateIfNotExists("feeds.json", "[]");

            var feedsJson = await File.ReadAllTextAsync("feeds.json");

            var feeds = JsonConvert.DeserializeObject<List<Feed>>(feedsJson)
                .ToDictionary(feed => feed.Name);

            feeds[feedName] = new Feed
            {
                Name = feedName,
                Url = feedUrl
            };

            var newFeedsJson = JsonConvert.SerializeObject(feeds.Values.ToList());

            await File.WriteAllTextAsync("feeds.json", newFeedsJson);

            return new CommandResult(true, $"Feed '{feedName}' saved");
        }
    }
}
