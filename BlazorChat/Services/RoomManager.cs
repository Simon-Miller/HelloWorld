using BlazorChat.Classes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlazorChat.Services
{
    public class RoomManager
    {
        public static event Action<Room> RoomsOccupantsChanged; 

        public static readonly EventDictionary<string, Room> Rooms = new EventDictionary<string, Room>();
        public static readonly EventList<Participant> Participants = new EventList<Participant>();

        public bool AddRoom(Room newRoom)
        {
            newRoom.Name = newRoom.Name.Trim();

            if (Rooms.Keys.Contains(newRoom.Name) == false)
            {
                Rooms.Add(newRoom.Name, newRoom);

                newRoom.OccupantsCount.ValueChanged += (O,N) => 
                {
                    if (O != N)
                    {
                        RoomsOccupantsChanged?.Invoke(newRoom);
                    }
                };
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// returns TRUE is participant was added.  FALSE means the name already exists, and is therefore not unique.
        /// </summary>
        public bool AddParticipant(Participant potenntiallyNewparticipant)
        {
            if (Participants.Any(x => x.DisplayName.ToLower() == potenntiallyNewparticipant.DisplayName.Trim().ToLower()) == false)
            {
                Participants.Add(potenntiallyNewparticipant);
                potenntiallyNewparticipant.DisplayName = potenntiallyNewparticipant.DisplayName.Trim();
                return true;
            }

            return false;
        }

        public void EnterAParticipantInRoom(Participant participant, Room room)
        {
            ParticipantLeaving(participant);
            participant.InRoom = room;

            room.OccupantsCount.Value++;
        }

        public void ParticipantLeaving(Participant participant)
        {
            if (participant != null && participant.InRoom != null)
                participant.InRoom.OccupantsCount.Value--;

            participant.InRoom = null;
        }

        public IReadOnlyCollection<Participant> GetOccupantsOfRoom(Room room)
        {
            return Participants.Where(p => p.InRoom == room).ToList();
        }

        /// <summary>
        /// the current user - which should survive refreshes thanks to SignalR.
        /// </summary>
        public Participant Participant = new Participant() { DisplayName = null };
    }

    public class Room
    {
        public Room()
        {
            // bubble up event to listeners.
            this.messages.ListChanged += (messages) => 
            {
                this.RoomMessagesChanged?.Invoke(messages);
            };
        }

        [Required]
        [RegularExpression(@"^[A-za-z -']{2,20}$", ErrorMessage = "between 2 to 20 characters, and must be unique.")]
        public string Name { get; set; }

        public EventProperty<int> OccupantsCount = new EventProperty<int>(startValue: 0);

        public event Action<IReadOnlyCollection<Message>> RoomMessagesChanged;

        /// <summary>
        /// a list of messages in the order they were posted.
        /// </summary>
        private readonly EventList<Message> messages = new EventList<Message>();

        public void AddMessage(Participant participant, string message)
        {
            this.messages.Add(new Message(participant, message));
        }

        public IReadOnlyCollection<Message> GetCurrentMessages => this.messages;
        
    }

    public class Participant
    {
        [Required]
        [RegularExpression(@"^[A-za-z -']{2,20}$", ErrorMessage = "between 2 to 20 characters, and must be unique.")]
        public string DisplayName { get; set; }

        public event Action<Room, Room> ChangedRoom;
        private Room room = null;
        public Room InRoom 
        {
            get => room; 
            set
            {
                var oldRoom = room;
                room = value;
                this.ChangedRoom?.Invoke(oldRoom, room);
            }
        }
    }

    /// <summary>
    /// in .NET 5 with C# 9, this could be a 'record'.
    /// It's immutable.
    /// </summary>
    public class Message
    {
        public Message(Participant participant, string messageEntered)
        {
            this.Participant = participant;
            this.MessageEntered = messageEntered;
            this.Posted = DateTime.Now;
        }

        public readonly Participant Participant;
        public readonly string MessageEntered;
        public readonly DateTime Posted;
    }
}
