namespace Sale.Models.DTOs
{
    public class CustomerReadDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
