using BlazorChatSimple.Classes.Utilities;

namespace BlazorChatSimple.Classes
{
    public class MyState
    {
        /// <summary>
        /// changes when user joins the conversation by providing a DisplayName
        /// </summary>
        public EventProperty<bool> Joined = new();
        public EventProperty<Participant> Partipant = new();
    }
}
