using System;
using Microsoft.Extensions.DependencyInjection;

namespace PodcastPlayer.CommandRouter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandRouting(this IServiceCollection serviceCollection, Func<CommandRouterBuilder, CommandRouter> builderFunc)
        {
            var builder = new CommandRouterBuilder(serviceCollection);

            var commandRouter = builderFunc(builder);

            serviceCollection.AddSingleton(commandRouter);

            return serviceCollection;
        }
    }
}
