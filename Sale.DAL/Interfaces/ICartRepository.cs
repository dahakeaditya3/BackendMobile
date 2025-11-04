using Sale.Models.Entities;
namespace Sale.DAL.Repositories
{
    public interface ICartRepository
    {
        Task<CartItem> AddAsync(CartItem item);
        Task<CartItem?> GetByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetByCustomerAsync(int customerId);
        Task UpdateAsync(CartItem item);
        Task DeleteAsync(CartItem item);
        Task ClearCartAsync(int customerId);
        Task<IEnumerable<CartItem>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
