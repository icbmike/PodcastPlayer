namespace PodcastPlayer.CommandRouter
{
    internal interface ICommandRoute
    {
        string Command { get; }

        string HelpText { get; }

        CommandResult Action(string commandText);
    }
}