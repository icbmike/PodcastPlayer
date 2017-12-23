using System.Collections.Generic;
using System.Threading.Tasks;

namespace PodcastPlayer.CommandRouter
{
    internal class CompositeCommand : ICommand
    {
        private string _command;
        private CommandRouter _router;

        public CompositeCommand(string commandName, IEnumerable<ICommand> subCommands)
        {
            _command = commandName;
        }

        public string Command => _command;

        public string HelpText => $"Run '{Command} help' to see help text for this set of commands.";

        public async Task<CommandResult> Action(string commandText)
        {
            return await _router.HandleCommand(commandText.ReplaceFirst(_command, "").Trim());
        }
    }
}
