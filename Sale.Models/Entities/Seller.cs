using System.ComponentModel.DataAnnotations;

namespace Sale.Models.Entities
{
    public class Seller
    {
        [Key]
        public int SellerId { get; set; }

        [Required, MaxLength(150)]
        public string SellerName { get; set; } = null!;

        [Required, MaxLength(150)]
        public string StoreName { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        [MaxLength(20)]
        public string Contact { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
