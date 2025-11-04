namespace Sale.Models.DTOs
{
    public class SellerReadDto
    {
        public int SellerId { get; set; }
        public string StoreName { get; set; }
        public string SellerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
