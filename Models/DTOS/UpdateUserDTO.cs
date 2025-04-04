namespace fullstack_backend.Models.DTOS
{
    public class UpdateUserDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
    }
}