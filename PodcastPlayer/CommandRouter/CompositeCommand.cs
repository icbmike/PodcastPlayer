using System.Collections.Generic;

namespace PodcastPlayer.CommandRouter
{
    internal class CompositeCommand : ICommandRoute
    {
        private string _command;
        private CommandRouter _router;

        public CompositeCommand(string commandName, IEnumerable<ICommandRoute> subCommands)
        {
            _command = commandName;
            _router = new CommandRouter(subCommands);
        }

        public string Command => _command;

        public string HelpText => $"Run '{Command} help' to see help text for this set of commands.";

        public CommandResult Action(string commandText)
        {
            return _router.HandleCommand(commandText.ReplaceFirst(_command, "").Trim());
        }
    }
}
