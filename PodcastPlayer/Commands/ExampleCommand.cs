using PodcastPlayer.CommandRouter;

namespace PodcastPlayer.Commands
{
    public class ExampleCommand : ICommandRoute
    {
        public string Command => "eg";

        public string HelpText => "Some example help text";

        public CommandResult Action(string commandText)
        {
            return new CommandResult(true, $"Your command was '{commandText}'");
        }
    }
}
