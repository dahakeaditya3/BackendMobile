using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db) => _db = db;

        public async Task<CartItem> AddAsync(CartItem item)
        {
            _db.CartItems.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _db.CartItems
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CartItem>> GetByCustomerAsync(int customerId)
        {
            return await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task UpdateAsync(CartItem item)
        {
            _db.CartItems.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(CartItem item)
        {
            _db.CartItems.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int customerId)
        {
            var items = _db.CartItems.Where(c => c.CustomerId == customerId);
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _db.CartItems
                .Include(c => c.Product)
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}
