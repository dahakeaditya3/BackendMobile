using Sale.DAL.Repositories;
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.BAL.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()

        {
            var products = await _repo.GetAllAsync();
            return products.Select(p => ToReadDto(p));
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : ToReadDto(p);
        }

        public async Task<int> CreateAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                ProductCompany = dto.ProductCompany,
                Price = dto.Price,
                Model=dto.Model, 
                OriginalPrice =dto.OriginalPrice,
                Condition =dto.Condition,
                Color =dto.Color,   
                Ram =dto.Ram,   
                Storage =dto.Storage,
                DisplaySize =dto.DisplaySize,
                BatteryCapacity =dto.BatteryCapacity,
                Camera =dto.Camera,
                OperatingSystem =dto.OperatingSystem,
                Network =dto.Network,
                Warranty =dto.Warranty,
                Description =dto.Description,
                SellerId = dto.SellerId,
                CreatedOn = DateTime.UtcNow,
                Image1 = await ConvertToBytes(dto.Image1),
                Image2 = await ConvertToBytes(dto.Image2),
                Image3 = await ConvertToBytes(dto.Image3),
                Image4 = await ConvertToBytes(dto.Image4)
            };

            await _repo.AddAsync(product);
            return product.ProductId;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");
            await _repo.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductReadDto>> GetBySellerAsync(int sellerId)
        {
            var products = await _repo.GetBySellerIdAsync(sellerId);
            return products.Select(p => ToReadDto(p));
        }

        private ProductReadDto ToReadDto(Product p) => new()
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            ProductCompany = p.ProductCompany,
            Price = p.Price,
            Model = p.Model,
            OriginalPrice = p.OriginalPrice,
            Condition = p.Condition,
            Color = p.Color,
            Ram = p.Ram,
            Storage = p.Storage,
            DisplaySize = p.DisplaySize,
            BatteryCapacity = p.BatteryCapacity,
            Camera = p.Camera,
            OperatingSystem = p.OperatingSystem,
            Network = p.Network,
            Warranty = p.Warranty,
            Description = p.Description,
            Image1Base64 = p.Image1 != null ? Convert.ToBase64String(p.Image1) : null,
            Image2Base64 = p.Image2 != null ? Convert.ToBase64String(p.Image2) : null,
            Image3Base64 = p.Image3 != null ? Convert.ToBase64String(p.Image3) : null,
            Image4Base64 = p.Image4 != null ? Convert.ToBase64String(p.Image4) : null,
            CreatedOn = p.CreatedOn,
            SellerId = p.SellerId,
            SellerName = p.Seller.SellerName
        };

        private async Task<byte[]?> ConvertToBytes(Microsoft.AspNetCore.Http.IFormFile? file)
        {
            if (file == null) return null;
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<bool> UpdateProductStatusAsync(ProductUpdateDto dto)
        {
            return await _repo.UpdateIsActiveAsync(dto.ProductId, dto.IsActive);
        }
    }
}
