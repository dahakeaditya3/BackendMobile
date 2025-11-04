namespace Sale.Dtos
{
        public class ProductRatingDto
        {
            public int RatingId { get; set; }
            public int RatingValue { get; set; }
            public string? Review { get; set; }
            public DateTime RatedOn { get; set; }
            public int ProductId { get; set; }
            public int CustomerId { get; set; }
            public string? CustomerName { get; set; }
        }

}
