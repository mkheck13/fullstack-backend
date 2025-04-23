using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_backend.Models
{
    public class MessageModel
    {
        public required string Sender { get; set; }
        public string receiver { get; set; }

        //the actual message and time it was sent
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; }

        //The following is in prep for when we have private messages between users

        //To create a Globally Unique ID or GUID
        // public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key that links the message to a conversation
        // public Guid ConversationId { get; set; }
        // A navigation property that represents the full Conversation object this message belongs to.
        // public Conversation Conversation { get; set; }
    }
}
