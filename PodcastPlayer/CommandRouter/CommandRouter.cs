using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastPlayer.CommandRouter
{
    public class CommandRouter
    {
        private readonly Dictionary<string, Type> _commands;
        private readonly IServiceProvider _serviceProvider;

        public CommandRouter(IServiceProvider serviceProvider, Dictionary<string, Type> commands)
        {
            _serviceProvider = serviceProvider;
            _commands = commands;
        }

        public async Task<CommandResult> HandleCommand(string commandText)
        {
            var commandParts = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var firstCommandPart = commandParts.FirstOrDefault();

            if (firstCommandPart == "exit")
            {
                return new CommandResult(false);
            }

            if(_commands.TryGetValue(firstCommandPart, out var commandType)) {
                var commandToExecute = (ICommand)_serviceProvider.GetService(commandType);

                return await commandToExecute.Action(commandText);
            }
            else
            {
                return new CommandResult(true, $"No command matched '{firstCommandPart}'. Use the 'help' command to see available commands or 'exit' to quit.");
            }
        }
    }
}