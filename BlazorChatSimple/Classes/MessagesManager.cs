using BlazorChatSimple.Classes.Utilities;

namespace BlazorChatSimple.Classes
{
    public class MessagesManager
    {
        public readonly EventList<Message> AllMessages = new EventList<Message>();

        public void PostMessage(string message, Participant poster)
        {
            poster.NumberOfPosts++;
            this.AllMessages.Add(new Message(message, poster.DisplayName));            
        }
    }
}
