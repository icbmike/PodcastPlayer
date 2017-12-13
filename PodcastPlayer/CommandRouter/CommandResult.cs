using System;

namespace PodcastPlayer.CommandRouter
{
    public class CommandResult
    {
        public CommandResult(bool shouldContinue, string message = null)
        {
            ShouldContinue = shouldContinue;
            Message = message;
        }

        public bool ShouldContinue { get; }

        public string Message { get; }

        public bool HasMessage => !String.IsNullOrWhiteSpace(Message);
    }
}