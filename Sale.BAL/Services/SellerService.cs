using Sale.DAL.Repositories;
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.BAL.Services
{
    public class SellerService
    {
        private readonly ISellerRepository _repo;
        public SellerService(ISellerRepository repo) => _repo = repo;

        public async Task<IEnumerable<SellerReadDto>> GetAllAsync()
        {
            var sellers = await _repo.GetAllAsync();
            return sellers.Select(s => new SellerReadDto
            {
                SellerId = s.SellerId,
                SellerName = s.SellerName,
                StoreName = s.StoreName,
                Email = s.Email,
                Contact = s.Contact,
                State = s.State,
                City = s.City,
                Address = s.Address,
                CreatedOn = s.CreatedOn
            });
        }


        public async Task<SellerReadDto?> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null) return null;

            return new SellerReadDto
            {
                SellerId = s.SellerId,
                SellerName = s.SellerName,
                StoreName = s.StoreName,
                Email = s.Email,
                Contact = s.Contact,
                State = s.State,
                City = s.City,
                Address = s.Address,
                CreatedOn = s.CreatedOn
            };
        }

        public async Task<SellerReadDto> CreateAsync(SellerCreateDto dto)
        {
            if (dto.Password != dto.ConfirmPassword) throw new Exception("Passwords do not match");
            if (await _repo.EmailExistsAsync(dto.Email)) throw new Exception("Email already exists");

            var seller = new Seller
            {
                SellerName = dto.SellerName,
                StoreName = dto.StoreName,
                Email = dto.Email,
                Contact = dto.Contact,
                State = dto.State,
                City = dto.City,
                Address = dto.Address,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedOn = DateTime.UtcNow
            };

            await _repo.AddAsync(seller);

            return new SellerReadDto
            {
                SellerId = seller.SellerId,
                SellerName = seller.SellerName,
                StoreName = seller.StoreName,
                Email = seller.Email,
                Contact = seller.Contact,
                State = seller.State,
                City = seller.City,
                Address = seller.Address,
                CreatedOn = seller.CreatedOn
            };
        }
        public async Task UpdateAsync(int id, SellerUpdateDto dto)
        {
            var seller = await _repo.GetByIdAsync(id);
            if (seller == null) throw new Exception("Seller not found");

            if (!string.IsNullOrWhiteSpace(dto.SellerName)) seller.SellerName = dto.SellerName;
            if (!string.IsNullOrWhiteSpace(dto.email)) seller.Email = dto.email;
            if (!string.IsNullOrWhiteSpace(dto.StoreName)) seller.StoreName = dto.StoreName;
            if (!string.IsNullOrWhiteSpace(dto.Contact)) seller.Contact = dto.Contact;
            if (!string.IsNullOrWhiteSpace(dto.State)) seller.State = dto.State;
            if (!string.IsNullOrWhiteSpace(dto.City)) seller.City = dto.City;
            if (!string.IsNullOrWhiteSpace(dto.Address)) seller.Address = dto.Address;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                if (dto.Password != dto.ConfirmPassword) throw new Exception("Passwords do not match");
                seller.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _repo.UpdateAsync(seller);
        }

        public async Task DeleteAsync(int id)
        {
            var seller = await _repo.GetByIdAsync(id);
            if (seller == null) throw new Exception("Seller not found");
            await _repo.DeleteAsync(seller);
        }

        public async Task<Seller?> GetByEmailAsync(string email)
        {
            return await _repo.GetByEmailAsync(email);
        }
    }
}



