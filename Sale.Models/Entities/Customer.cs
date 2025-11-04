using System.ComponentModel.DataAnnotations;

namespace Sale.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, MaxLength(150)]
        public string CustomerName { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Contact { get; set; }

        [Required, MaxLength(150)]
        public string Gender { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(150)]
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
