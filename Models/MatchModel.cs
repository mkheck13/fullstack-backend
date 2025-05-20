using System.ComponentModel.DataAnnotations;

namespace fullstack_backend.Models
{
    public class MatchModel
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public string? MyName { get; set; }
        public string? UserContent { get; set; }
        public string? UserSport { get; set; }
        public string? DateCreated { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}