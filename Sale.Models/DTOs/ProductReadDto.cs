namespace Sale.Models.DTOs
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductCompany { get; set; } = null!;
        public decimal Price { get; set; }
        public string Model { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string Condition { get; set; }
        public string Color { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public double? DisplaySize { get; set; }
        public string? BatteryCapacity { get; set; }
        public string? Camera { get; set; }
        public string OperatingSystem { get; set; }
        public string Network { get; set; }
        public string? Warranty { get; set; }
        public string Description { get; set; }
        public string? Image1Base64 { get; set; }
        public string? Image2Base64 { get; set; }
        public string? Image3Base64 { get; set; }
        public string? Image4Base64 { get; set; }
        public DateTime CreatedOn { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; } = null!;
    }
}
