using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastPlayer.CommandRouter
{
    public class CommandRouter
    {
        private readonly Dictionary<string, Func<string, ICommand>> _commands;


        public CommandRouter(Dictionary<string, Func<string, ICommand>> commands)
        {
            _commands = commands;
        }

        public async Task<CommandResult> HandleCommand(string commandText)
        {
            var commandParts = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var firstCommandPart = commandParts.FirstOrDefault() ?? "";

            if (firstCommandPart == "exit")
            {
                return new CommandResult(false);
            }

            if(_commands.TryGetValue(firstCommandPart, out var commandProvider)) {
                var restOfCommand = string.Join(" ", commandParts.Skip(1));
                var commandToExecute = commandProvider(restOfCommand);

                try
                {
                    return await commandToExecute.Action(restOfCommand);
                }
                catch (Exception e){
                    return new CommandResult(false, e.Message);
                }
            }
            else
            {
                return new CommandResult(true, $"No command matched '{firstCommandPart}'. Use the 'help' command to see available commands or 'exit' to quit.");
            }
        }
    }
}