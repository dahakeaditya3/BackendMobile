namespace Sale.Models.DTOs
{
    public class SellerCreateDto
    {
        public string SellerName { get; set; } = null!;
        public string StoreName { get; set; }
        public string Email { get; set; } = null!;
        public string Contact { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
