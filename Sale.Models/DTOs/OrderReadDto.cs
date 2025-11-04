namespace Sale.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductCompany { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = null!;
        public ProductImages Images { get; set; } = null!;
        public SellerInfo Seller { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string ReceiverContactNumber { get; set; } = null!;
        public string OrderAddress { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class ProductImages
    {
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
    }

    public class SellerInfo
    {
        public string SellerName { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
