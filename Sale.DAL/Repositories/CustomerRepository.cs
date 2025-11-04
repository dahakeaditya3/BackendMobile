using Microsoft.EntityFrameworkCore;
using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;
        public CustomerRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _db.Customers.ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _db.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.CustomerId == id);

        public async Task AddAsync(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _db.Customers.AnyAsync(c =>
                c.Email == email && (!excludeId.HasValue || c.CustomerId != excludeId.Value));
        }
        public async Task<Customer?> GetByEmailAsync(string email) =>
    await _db.Customers.FirstOrDefaultAsync(c => c.Email == email);

    }
}
