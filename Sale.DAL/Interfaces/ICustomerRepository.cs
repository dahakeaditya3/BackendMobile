using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        Task<Customer?> GetByEmailAsync(string email);
    }
}
