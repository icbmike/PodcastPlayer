using System;
using System.Collections.Generic;

namespace PodcastPlayer
{
    class CompositeCommandRoute : ICommandRoute
    {
        private string _command;
        private CommandRouter _router;

        public CompositeCommandRoute(string commandName, IEnumerable<ICommandRoute> subCommands)
        {
            _command = commandName;
            _router = new CommandRouter(subCommands);
        }

        public string Command => _command;

        public string HelpText => $"Run '{Command} help' to see help text for this set of commands.";

        public void Action(string commandText)
        {
            _router.HandleCommand(commandText.ReplaceFirst(_command, "").Trim());
        }
    }
}
