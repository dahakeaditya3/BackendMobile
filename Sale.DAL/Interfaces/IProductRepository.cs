using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> GetBySellerIdAsync(int sellerId);
        Task<bool> UpdateIsActiveAsync(int productId, bool isActive);
    }
}
