using System;
using System.Collections.Generic;

namespace PodcastPlayer
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

        public void Action(string commandText)
        {
            Console.WriteLine("Commands available:");

            foreach(var command in _otherCommands)
            {
                Console.WriteLine($"{command.Command} - {command.HelpText}");
            }
        }
    }
}