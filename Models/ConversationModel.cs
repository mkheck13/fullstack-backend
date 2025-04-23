using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using fullstack_backend.Models;

namespace fullstack_backend.Models
{
    public class ConversationModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string User1Id { get; set; }
        public string User2Id { get; set; }

        public List<MessageModel> Messages { get; set; } = new();

        // public Guid Id { get; set; } = Guid.NewGuid();

        // Assuming private 1-on-1 chat. We don't want to tackle group chats as of yet
        // public string User1Id { get; set; }
        // public string User2Id { get; set; }

        /*
        Note:
        1. ICollection<T> is an interface that represents any list-like collection (e.g. List<T>, HashSet<T>).
        2. EF Core recognizes ICollection<T> as a navigable relationship, so it knows how to map this to a foreign key table (Message).
        3. This is known as a one-to-many relationship, so there is "one" converseration, but "many" messages in the conversation
        */
        // public ICollection<Message> Messages { get; set; } = new List<Message>();
        // public List<MessageModel> Messages { get; set; }
    }
}
