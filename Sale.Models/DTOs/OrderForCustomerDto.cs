namespace Sale.Models.Dtos
{
    public class OrderForCustomerDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCompany { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public ProductImagesDto Images { get; set; } = new();
        public SellerDto Seller { get; set; } = new();
        public string ReceiverName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class ProductForCustomerDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public ProductImagesDto Images { get; set; } = null!;
        public SellerDto Seller { get; set; } = null!;
    }

    public class ProductImagesDto
    {
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
    }

    public class SellerDto
    {
        public string SellerName { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
