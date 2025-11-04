namespace Sale.Models.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string Role { get; set; } = null!;
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
    }
}
