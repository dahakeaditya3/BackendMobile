using Sale.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sale.DAL.Repositories
{
    public interface IProductRatingRepository
    {
        Task AddRatingAsync(ProductRating rating);
        Task<IEnumerable<ProductRating>> GetRatingsByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
    }
}
