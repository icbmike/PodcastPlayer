using System;
using Microsoft.Extensions.DependencyInjection;

namespace PodcastPlayer.CommandRouter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandRouting(this IServiceCollection serviceCollection, Func<CommandRouterBuilder, CommandRouterBuilder> builderFunc)
        {
            var builder = builderFunc(new CommandRouterBuilder(serviceCollection));

            serviceCollection.AddSingleton((arg) => builder.Build());

            return serviceCollection;
        }
    }
}
