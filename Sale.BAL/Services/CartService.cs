using Sale.DAL.Repositories;
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.BAL.Services
{
    public class CartService
    {
        private readonly ICartRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly OrderService _orderService;

        public CartService(ICartRepository repo, IProductRepository productRepo, OrderService orderService)
        {
            _repo = repo;
            _productRepo = productRepo;
            _orderService = orderService;
        }

        public async Task<CartItemReadDto> AddToCartAsync(CartItemCreateDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product == null) throw new Exception("Product not found");

            var item = new CartItem
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity > 0 ? dto.Quantity : 1,
                UnitPrice = product.Price,
                CreatedOn = DateTime.UtcNow
            };

            var saved = await _repo.AddAsync(item);

            return new CartItemReadDto
            {
                Id = saved.Id,
                CustomerId = saved.CustomerId,
                ProductId = saved.ProductId,
                ProductName = saved.Product.ProductName,
                ProductCompany = saved.Product.ProductCompany,
                Image1Base64 = saved.Product.Image1 != null ? Convert.ToBase64String(saved.Product.Image1) : null,
                Quantity = saved.Quantity,
                UnitPrice = saved.UnitPrice,
                CreatedOn = saved.CreatedOn
            };
        }

        public async Task<IEnumerable<CartItemReadDto>> GetCartAsync(int customerId)
        {
            var items = await _repo.GetByCustomerAsync(customerId);
            return items.Select(i => new CartItemReadDto
            {
                Id = i.Id,
                CustomerId = i.CustomerId,
                ProductId = i.ProductId,
                ProductName = i.Product.ProductName,
                ProductCompany = i.Product.ProductCompany,
                Image1Base64 = i.Product.Image1 != null ? Convert.ToBase64String(i.Product.Image1) : null,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                CreatedOn = i.CreatedOn
            });
        }
        public async Task<bool> UpdateItemAsync(CartItemUpdateDto dto)
        {
            var item = await _repo.GetByIdAsync(dto.Id);
            if (item == null) return false;
            if (dto.Quantity <= 0) return false;
            item.Quantity = dto.Quantity;
            await _repo.UpdateAsync(item);
            return true;
        }

        public async Task<bool> RemoveItemAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return false;
            await _repo.DeleteAsync(item);
            return true;
        }

        public async Task<bool> CheckoutFromCartAsync(BulkOrderCreateDto dto)
        {
            var items = (await _repo.GetByIdsAsync(dto.CartItemIds)).ToList();

            if (!items.Any()) return false;

            var created = await _orderService.CreateOrdersFromCartAsync(dto.CustomerId, items);
            if (!created) return false;

            foreach (var it in items)
                await _repo.DeleteAsync(it);

            return true;
        }
    }
}
