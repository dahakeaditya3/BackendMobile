using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sale.Models.Entities
{
    public class ProductRating
    {
        [Key]
        public int RatingId { get; set; }

        [Required, Range(1, 5)]
        public int RatingValue { get; set; }

        [MaxLength(500)]
        public string? Review { get; set; }

        public DateTime RatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
