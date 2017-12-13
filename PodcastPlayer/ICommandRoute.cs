namespace PodcastPlayer
{
    internal interface ICommandRoute
    {
        string Command { get; }

        string HelpText { get; }

        void Action(string commandText);
    }
}