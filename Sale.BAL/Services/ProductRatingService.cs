using Sale.DAL;
using Sale.DAL.Repositories;
using Sale.Dtos;
using Sale.Models.Entities;

namespace Sale.BAL.Services
{
    public class ProductRatingService
    {
        private readonly IProductRatingRepository _ratingRepo;
        private readonly AppDbContext _context;

        public ProductRatingService(IProductRatingRepository ratingRepo, AppDbContext context)
        {
            _ratingRepo = ratingRepo;
            _context = context;
        }

        public async Task AddRatingAsync(ProductRatingDto dto)
        {
            var rating = new ProductRating
            {
                ProductId = dto.ProductId,
                CustomerId = dto.CustomerId,
                RatingValue = dto.RatingValue,
                Review = dto.Review
            };

            await _ratingRepo.AddRatingAsync(rating);

            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product != null)
            {
                product.AverageRating = await _ratingRepo.GetAverageRatingAsync(dto.ProductId);
                product.RatingCount = product.RatingCount + 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            return await _ratingRepo.GetAverageRatingAsync(productId);
        }

        public async Task<IEnumerable<ProductRating>> GetRatingsAsync(int productId)
        {
            return await _ratingRepo.GetRatingsByProductIdAsync(productId);
        }


    }
}
