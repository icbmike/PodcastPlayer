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
        private readonly Dictionary<string, CommandRouterBuilder> _compositeRoutes;

        public CommandRouterBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _commands = new Dictionary<string, Type>();
            _compositeRoutes = new Dictionary<string, CommandRouterBuilder>();
        }

        public CommandRouterBuilder RegisterRoute<TCommand>(string command) where TCommand : class, ICommand
        {
            var commandType = typeof(TCommand);

            _commands.Add(command, commandType);
            _serviceCollection.AddTransient(commandType);

            return this;
        }

        public CommandRouterBuilder RegisterCompositeRoute(string command, Func<CommandRouterBuilder, CommandRouterBuilder> compositeBuilderFunc)
        {
            var compositeBuilder = compositeBuilderFunc(new CommandRouterBuilder(_serviceCollection));

            _compositeRoutes
                .Add(command, compositeBuilder);
            
            return this;
        }

        public CommandRouterBuilder AddHelp(string helpCommand = "help")
        {
            _commands.Add(helpCommand, typeof(HelpCommand));

            _serviceCollection.AddTransient(serviceProvider =>
            {
                var commandsDict = _commands
                    .Where(kvp => kvp.Key != helpCommand)
                    .Select(kvp => KeyValuePair.Create(kvp.Key, (ICommand)serviceProvider.GetService(kvp.Value)))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                return new HelpCommand(commandsDict);
            });

            return this;
        }

        public CommandRouter Build()
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();

            var regularCommands = _commands
                .Select(kvp => KeyValuePair.Create<string, Func<string, ICommand>>(
                    kvp.Key,
                    (string str) => (ICommand)serviceProvider.GetService(kvp.Value)
                ));

            var compositeCommands = _compositeRoutes
                .Select(kvp =>
                {
                    var router = kvp.Value.Build();

                    return KeyValuePair.Create<string, Func<string, ICommand>>(
                        kvp.Key,
                        (string str) => new CompositeCommand(str, router)
                    );
                });

            var commandProviderDict = regularCommands
                .Concat(compositeCommands)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return new CommandRouter(commandProviderDict);
        }
    }
}
