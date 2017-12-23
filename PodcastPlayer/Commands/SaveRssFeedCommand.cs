using System;
using System.Threading.Tasks;
using PodcastPlayer.CommandRouter;

namespace PodcastPlayer.Commands
{
    public class SaveRssFeedCommand : ICommand
    {
        public string HelpText => "Pass a url to an rss feed. Will save it.";

        public async Task<CommandResult> Action(string commandText)
        {
            return new CommandResult(true);   
        }
    }
}
