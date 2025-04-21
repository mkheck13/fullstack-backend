namespace fullstack_backend.Models
{
    public class UserModel
    {
        public int Id { get; set;}
        public string? Username { get; set;}
        public string? Email { get; set;}
        public string? Salt { get; set;}
        public string? Hash { get; set;}
        public string? DateOfBirth { get; set;}
        public string? PhoneNumber { get; set;}
        public string? ProfilePicture { get; set;}

        // new updates
        public string? UserBio { get; set;}
        public string? UserLocation { get; set;}
        public bool UserLocationPublic { get; set;}
        public string? UserPrimarySport { get; set;}
        public string? UserSecondarySport { get; set;}
    }
}