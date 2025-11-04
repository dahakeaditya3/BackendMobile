using Microsoft.IdentityModel.Tokens;
using Sale.Models.DTOs;
using Sale.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Sale.BAL.Services
{
    public class AuthService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ISellerRepository _sellerRepo;
        private readonly IConfiguration _config;

        public AuthService(ICustomerRepository customerRepo, ISellerRepository sellerRepo, IConfiguration config)
        {
            _customerRepo = customerRepo;
            _sellerRepo = sellerRepo;
            _config = config;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            if (dto.Role.ToLower() == "customer")
            {
                var customer = await _customerRepo.GetByEmailAsync(dto.Email);
                if (customer == null || !BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash))
                    return null;

                var token = GenerateJwtToken(customer.CustomerId, customer.Email, "Customer");
                return new AuthResponseDto
                {
                    Token = token.Item1,
                    Expiration = token.Item2,
                    Role = "Customer",
                    UserId = customer.CustomerId,
                    Email = customer.Email
                };
            }
            else if (dto.Role.ToLower() == "seller")
            {
                var seller = await _sellerRepo.GetByEmailAsync(dto.Email);
                if (seller == null || !BCrypt.Net.BCrypt.Verify(dto.Password, seller.PasswordHash))
                    return null;

                var token = GenerateJwtToken(seller.SellerId, seller.Email, "Seller");
                return new AuthResponseDto
                {
                    Token = token.Item1,
                    Expiration = token.Item2,
                    Role = "Seller",
                    UserId = seller.SellerId,
                    Email = seller.Email
                };
            }

            throw new Exception("Role must be Customer or Seller.");
        }

        private (string, DateTime) GenerateJwtToken(int userId, string email, string role)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId.ToString()),
                new Claim("Role", role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
