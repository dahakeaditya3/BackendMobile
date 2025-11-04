using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sale.Models.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } 

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(CustomerId))]
         public Customer Customer { get; set; }
    }
}
