namespace Sale.Models.DTOs
{
    public class CartItemCreateDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class CartItemUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCompany { get; set; }
        public string? Image1Base64 { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public DateTime CreatedOn { get; set; }
    }

    public class BulkOrderCreateDto
    {
        public int CustomerId { get; set; }
        public List<int> CartItemIds { get; set; } = new();
    }
}
