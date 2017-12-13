using System;

namespace PodcastPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandRouter = new CommandRouter(new ICommandRoute[] {
                new CompositeCommandRoute("podcasts", new[]
                {
                    new ExampleCommand()
                }),
                new ExampleCommand()
            });

            var executeHandleCommand = true;
            while (executeHandleCommand)
            {
                Console.Write("PodcastPlayer> ");
                executeHandleCommand = commandRouter.HandleCommand(Console.ReadLine());
                Console.WriteLine();
            }
        }
    }
}
