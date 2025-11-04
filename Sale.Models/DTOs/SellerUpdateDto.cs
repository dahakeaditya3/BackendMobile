namespace Sale.Models.DTOs
{
    public class SellerUpdateDto
    {
        public string? SellerName { get; set; } = null!;
        public string? email { get; set; }
        public string? StoreName { get; set; }
        public string? Contact { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
