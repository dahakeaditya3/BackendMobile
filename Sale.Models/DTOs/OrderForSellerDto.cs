using System.ComponentModel.DataAnnotations;

namespace Sale.Models.Dtos
{
    public class OrderForSellerDto
    {
        public int Id { get; set; }
        public CustomerDto Customer { get; set; } = null!;
        public ProductForSellerDto Product { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
        public string ReceiverContactNumber { get; set; } = null!;
        public string OrderAddress { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class ProductForSellerDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public decimal Price { get; set; }
        public ProductImagesDto Images { get; set; } = null!;
        public SellerInfoDto Seller { get; set; } = null!;
    }

    public class SellerInfoDto
    {
        public int SellerId { get; set; }
        public string SellerName { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
