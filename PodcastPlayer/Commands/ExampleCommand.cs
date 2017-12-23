using System.Threading.Tasks;
using PodcastPlayer.CommandRouter;

namespace PodcastPlayer.Commands
{
    public class ExampleCommand : ICommand
    {
        public string Command => "eg";

        public string HelpText => "Some example help text";

        public async Task<CommandResult> Action(string commandText)
        {
            return new CommandResult(true, $"Your command was '{commandText}'");
        }
    }
}
