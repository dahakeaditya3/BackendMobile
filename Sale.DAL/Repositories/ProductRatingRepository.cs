using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        private readonly AppDbContext _context;

        public ProductRatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRatingAsync(ProductRating rating)
        {
            _context.ProductRatings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductRating>> GetRatingsByProductIdAsync(int productId)
        {
            return await _context.ProductRatings
                 .Include(r => r.Customer)
                 .Where(r => r.ProductId == productId)
                 .OrderByDescending(r => r.RatedOn)
                 .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            var ratings = await _context.ProductRatings
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            return ratings.Count == 0 ? 0 : ratings.Average(r => r.RatingValue);
        }
    }
}
