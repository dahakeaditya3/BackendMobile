namespace Sale.Models.DTOs
{
    public class CustomerCreateDto
    {
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
