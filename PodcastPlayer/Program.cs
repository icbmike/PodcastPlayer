using System;
using Microsoft.Extensions.DependencyInjection;
using PodcastPlayer.CommandRouter;
using PodcastPlayer.Commands;

namespace PodcastPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            UseCommandRouter(serviceProvider.GetService<CommandRouter.CommandRouter>());
        }

        private static void UseCommandRouter(CommandRouter.CommandRouter commandRouter)
        {
            var executeHandleCommand = true;
            while (executeHandleCommand)
            {
                Console.Write("PodcastPlayer> ");

                var result = commandRouter.HandleCommand(Console.ReadLine()).Result;
                executeHandleCommand = result.ShouldContinue;

                if (result.HasMessage)
                {
                    Console.WriteLine(result.Message);
                }

                Console.WriteLine();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRssService, RssService>();

            services.AddCommandRouting(builder =>
            {
                builder.AddHelp();
                builder.RegisterRoute<QueryRssFeedCommand>("rss");

                return builder.Build();
            });
        }
    }
}
