using Sale.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sale.DTOs
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string ReceiverName { get; set; } = null!;

        public string ReceiverContactNumber { get; set; } = null!;

        public string OrderAddress { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string? Status { get; set; }


    }
}
