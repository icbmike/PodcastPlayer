using System;
using System.Linq;
using System.Collections.Generic;

namespace PodcastPlayer.CommandRouter
{
    internal class HelpCommand : ICommandRoute
    {
        private readonly IEnumerable<ICommandRoute> _otherCommands;

        public HelpCommand(IEnumerable<ICommandRoute> otherCommands)
        {
            _otherCommands = otherCommands;
        }

        public string HelpText => "You're looking at it.";

        public string Command => "help";

        public CommandResult Action(string commandText)
        {
            return new CommandResult(true, "Commands available: \n" +
                string.Join("\n", _otherCommands.Select(command => $"{command.Command} - {command.HelpText}")));
        }
    }
}