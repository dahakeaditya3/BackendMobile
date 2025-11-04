using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Sale.Models.DTOs
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; } = null!;
        public string ProductCompany { get; set; } = null!;
        public decimal Price { get; set; }
        public string Model { get; set; }
        public decimal OriginalPrice { get; set; }
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
        public int SellerId { get; set; }
        public IFormFile? Image1 { get; set; }
        public IFormFile? Image2 { get; set; }
        public IFormFile? Image3 { get; set; }
        public IFormFile? Image4 { get; set; }
    }
}
