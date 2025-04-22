namespace fullstack_backend.Models.DTOS
{
    public class UserDTO
    {
        public string? Username {get; set;}
        public string? Email { get; set;}
        public string? Password {get; set;}
        public string? DateOfBirth { get; set;}
        public string? PhoneNumber { get; set;}

        public bool IsTrainer { get; set;} 
        public bool IsSpotter { get; set;} 
    }
}