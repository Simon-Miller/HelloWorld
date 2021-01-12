using BlazorChatSimple.Classes.Utilities;
using System.Linq;

namespace BlazorChatSimple.Classes
{
    public class ParticipantsManager
    {
        public readonly EventList<Participant> AllParticipants = new EventList<Participant>();

        /// <summary>
        /// tries to add your <paramref name="displayName"/> to the collection, ensuring it will be unique.
        /// If it already exists, then FALSE will be returned.
        /// </summary>
        public bool TryJoin(Participant potentialParticipant)
        {
            var trimmedName = potentialParticipant.DisplayName.Trim();

            var comparisonName = trimmedName.ToLower();

            if (AllParticipants.Any(x => x.DisplayName.ToLower() == comparisonName))
            {
                return false;
            }
            else
            {
                potentialParticipant.DisplayName = trimmedName;
                AllParticipants.Add(potentialParticipant);

                return true;
            }
        }
    }
}
