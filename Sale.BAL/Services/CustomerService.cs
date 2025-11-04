using Sale.DAL.Repositories;
using Sale.Models.DTOs;
using Sale.Models.Entities;
using BCrypt.Net;

namespace Sale.BAL.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo) => _repo = repo;

        public async Task<IEnumerable<CustomerReadDto>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            return customers.Select(c => new CustomerReadDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                Email = c.Email,
                Contact = c.Contact,
                Gender = c.Gender,
                City = c.City,
                CreatedOn = c.CreatedOn
            });
        }

        public async Task<CustomerReadDto?> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            return new CustomerReadDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                Email = c.Email,
                Contact = c.Contact,
                Gender = c.Gender,
                City = c.City,
                CreatedOn = c.CreatedOn
            };
        }

        public async Task<CustomerReadDto> CreateAsync(CustomerCreateDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            if (await _repo.EmailExistsAsync(dto.Email))
                throw new Exception("Email already exists");

            var customer = new Customer
            {
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                Contact = dto.Contact,
                Gender = dto.Gender,
                City = dto.City,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedOn = DateTime.UtcNow
            };

            await _repo.AddAsync(customer);

            return new CustomerReadDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                Contact = customer.Contact,
                Gender = customer.Gender,
                City = customer.City,
                CreatedOn = customer.CreatedOn
            };
        }

        public async Task UpdateAsync(int id, CustomerUpdateDto dto)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");

            if (!string.IsNullOrWhiteSpace(dto.CustomerName)) customer.CustomerName = dto.CustomerName;
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                if (await _repo.EmailExistsAsync(dto.Email, id))
                    throw new Exception("Email already exists");
                customer.Email = dto.Email;
            }
            if (!string.IsNullOrWhiteSpace(dto.Contact)) customer.Contact = dto.Contact;
            if (!string.IsNullOrWhiteSpace(dto.Gender)) customer.Gender = dto.Gender;
            if (!string.IsNullOrWhiteSpace(dto.City)) customer.City = dto.City;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                if (dto.Password != dto.ConfirmPassword)
                    throw new Exception("Passwords do not match");
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _repo.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer not found");
            await _repo.DeleteAsync(customer);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _repo.GetByEmailAsync(email);
        }
    }
}
