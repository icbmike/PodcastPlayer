using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace PodcastPlayer.CommandRouter
{
    public class CommandRouterBuilder
    {
        readonly IServiceCollection _serviceCollection;
        private readonly Dictionary<string, Type> _commands;

        public CommandRouterBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _commands = new Dictionary<string, Type>();
        }

        public void RegisterRoute<TCommand>(string command) where TCommand : class, ICommand
        {
            var commandType = typeof(TCommand);

            _commands.Add(command, commandType);
            _serviceCollection.AddTransient(commandType);
        }

        public void AddHelp(string helpCommand = "help"){
            _commands.Add(helpCommand, typeof(HelpCommand));

            _serviceCollection.AddTransient(serviceProvider => {
                var commandsDict = _commands
                    .Where(kvp => kvp.Key != helpCommand)
                    .Select(kvp => KeyValuePair.Create(kvp.Key, (ICommand)serviceProvider.GetService(kvp.Value)))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                
                return new HelpCommand(commandsDict);
            });
        }

        public CommandRouter Build()
        {
            return new CommandRouter(_serviceCollection.BuildServiceProvider(), _commands);
        }
    }
}
