using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;
using Sale.DAL;

namespace Sale.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _db.Products.Include(p => p.Seller)
            .Where(p => p.IsActive == true).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _db.Products.Include(p => p.Seller).FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UpdateIsActiveAsync(int productId, bool isActive)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
                return false;

            product.IsActive = isActive;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetBySellerIdAsync(int sellerId) =>
            await _db.Products.Include(p => p.Seller)
                              .Where(p => p.SellerId == sellerId && p.IsActive==true)
                              .ToListAsync();

    }
}
