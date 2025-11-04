using Sale.Models.Entities;

namespace Sale.DAL.Repositories
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAllAsync();
        Task<Seller?> GetByIdAsync(int id);
        Task AddAsync(Seller seller);
        Task UpdateAsync(Seller seller);
        Task DeleteAsync(Seller seller);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        Task<Seller?> GetByEmailAsync(string email);
    }
}
