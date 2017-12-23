using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PodcastPlayer.CommandRouter
{
    internal class HelpCommand : ICommand
    {
        private readonly Dictionary<string, ICommand> _otherCommands;

        public HelpCommand(Dictionary<string, ICommand> otherCommands)
        {
            _otherCommands = otherCommands;
        }

        public string HelpText => "You're looking at it.";

        public string Command => "help";

        public Task<CommandResult> Action(string commandText)
        {
            return Task.FromResult(
                new CommandResult(
                    true, 
                    "Commands available: \n" + string.Join("\n", _otherCommands.Select(command => $"{command.Key} - {command.Value.HelpText}"))
                )
            );
        }
    }
}