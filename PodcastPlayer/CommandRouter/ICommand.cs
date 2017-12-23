using System.Threading.Tasks;

namespace PodcastPlayer.CommandRouter
{
    public interface ICommand
    {
        string HelpText { get; }

        Task<CommandResult> Action(string commandText);
    }
}