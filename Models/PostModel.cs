namespace fullstack_backend.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? TrueName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? DateCreated { get; set; }
        public string? Description { get; set; }
        public string? Stat { get; set; }
        public string? Sport { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
    }
}
