using System;
using System.Collections.Generic;
using System.Linq;

namespace PodcastPlayer
{
    internal class CommandRouter
    {
        private readonly IEnumerable<ICommandRoute> _routes;

        public CommandRouter(IEnumerable<ICommandRoute> routes)
        {
            _routes = routes.Concat(new[] { new HelpCommand(routes) });
        }

        internal bool HandleCommand(string commandText)
        {
            var commandParts = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var firstCommandPart = commandParts.FirstOrDefault();

            if (firstCommandPart == "exit")
            {
                return false;
            }

            var commandToExecute = _routes.FirstOrDefault(route => route.Command == firstCommandPart);

            if (commandToExecute != null)
            {
                commandToExecute.Action(commandText);
            }
            else
            {
                Console.WriteLine($"No command matched '{firstCommandPart}'. Use the 'help' command to see available commands or 'exit' to quit.");
            }

            return true;
        }
    }
}