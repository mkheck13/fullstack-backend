using System.ComponentModel.DataAnnotations.Schema;

namespace fullstack_backend.Models
{
    public class FriendshipModel
    {
        public int Id { get; set; }

        // The user who initiated the friend request
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        // The user who received the friend request
        public int FriendId { get; set; }

        [ForeignKey("FriendId")]
        public UserModel Friend { get; set; }

        public bool IsConfirmed { get; set; } = false;
    }
}