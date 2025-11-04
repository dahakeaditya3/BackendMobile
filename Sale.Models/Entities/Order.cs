using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sale.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required, MaxLength(150)]
        public string ReceiverName { get; set; } = null!;

        [Required, MaxLength(15)]
        public string ReceiverContactNumber { get; set; } = null!;

        [Required, MaxLength(300)]
        public string OrderAddress { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow.AddDays(7);
    }
}