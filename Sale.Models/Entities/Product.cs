using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sale.Dtos;
using Sale.Models.Entities;

namespace Sale.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(200)]
        public string ProductName { get; set; } = null!;

        [Required, MaxLength(200)]
        public string ProductCompany { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required, MaxLength(50)]
        public string Model { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        [Required, MaxLength(20)]
        public string Condition { get; set; }

        [MaxLength(20)]
        public string Color { get; set; }

        public string Ram { get; set; }
        public string Storage { get; set; }
        public double? DisplaySize { get; set; }
        public string? BatteryCapacity { get; set; }
        [MaxLength(100)]
        public string? Camera { get; set; }
        [MaxLength(100)]
        public string OperatingSystem { get; set; }
        [MaxLength(50)]
        public string Network { get; set; }
        [MaxLength(50)]
        public string? Warranty { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public byte[]? Image1 { get; set; }
        public byte[]? Image2 { get; set; }
        public byte[]? Image3 { get; set; }
        public byte[]? Image4 { get; set; }

       
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsActive { get; set; } = true;

        public int SellerId { get; set; }
        public Seller Seller { get; set; } = null!;

        public double? AverageRating { get; set; } = 0;
        public int RatingCount { get; set; } = 0;
        public ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();


        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}




