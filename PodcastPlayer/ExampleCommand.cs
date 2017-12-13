using System;

namespace PodcastPlayer
{
    class ExampleCommand : ICommandRoute
    {
        public string Command => "eg";

        public string HelpText => "Some example help text";

        public void Action(string commandText)
        {
            Console.WriteLine($"Your command was '{commandText}'");
        }
    }
}
