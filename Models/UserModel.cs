namespace fullstack_backend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? UserBio { get; set; }
        public string? UserLocation { get; set; }
        public bool UserLocationPublic { get; set; }
        public string? UserPrimarySport { get; set; }
        public string? UserSecondarySport { get; set; }
        public bool IsTrainer { get; set; }
        public bool IsSpotter { get; set; }
        public string? TrueName { get; set; }

        public ICollection<FriendshipModel> FriendsRequested { get; set; } = new List<FriendshipModel>();
        public ICollection<FriendshipModel> FriendsReceived { get; set; } = new List<FriendshipModel>();

    }
}