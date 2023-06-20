namespace PRO.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }

        public required string PasswordHash { get; set; } 
    }
}
