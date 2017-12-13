using PodcastPlayer.CommandRouter;
using PodcastPlayer.Commands;
using System;

namespace PodcastPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandRouter = new CommandRouter.CommandRouter(new ICommandRoute[] {
                new QueryRssFeedCommand()
            });

            var executeHandleCommand = true;
            while (executeHandleCommand)
            {
                Console.Write("PodcastPlayer> ");

                var result = commandRouter.HandleCommand(Console.ReadLine());
                executeHandleCommand = result.ShouldContinue;

                if (result.HasMessage)
                {
                    Console.WriteLine(result.Message);
                }

                Console.WriteLine();
            }
        }
    }
}
