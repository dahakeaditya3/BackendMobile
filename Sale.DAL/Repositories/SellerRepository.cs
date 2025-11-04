using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AppDbContext _db;
        public SellerRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Seller>> GetAllAsync() =>
            await _db.Sellers.ToListAsync();

        public async Task<Seller?> GetByIdAsync(int id) =>
            await _db.Sellers.Include(s => s.Products).FirstOrDefaultAsync(s => s.SellerId == id);

        public async Task AddAsync(Seller seller)
        {
            _db.Sellers.Add(seller);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            _db.Sellers.Update(seller);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seller seller)
        {
            _db.Sellers.Remove(seller);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _db.Sellers.AnyAsync(s =>
                s.Email == email && (!excludeId.HasValue || s.SellerId != excludeId.Value));
        }
        public async Task<Seller?> GetByEmailAsync(string email) =>
     await _db.Sellers.FirstOrDefaultAsync(s => s.Email == email);
    }
}
