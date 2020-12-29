using System;

namespace BlazorChatSimple.Classes
{
    public class Message
    {
        public Message(string post, string participantDisplayName)
        {
            this.Post = post;
            this.ParticipantName = participantDisplayName;
            this.PostedDateTime = DateTime.Now;
        }

        public readonly string Post;
        public readonly DateTime PostedDateTime;
        public readonly string ParticipantName;
    }
}
