using Microsoft.EntityFrameworkCore;
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public async Task<List<Order>> GetAllOrdersAsync() =>
            await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                    .ThenInclude(p => p.Seller)
                .ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(int id) =>
                await _db.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Product)
                        .ThenInclude(p => p.Seller)
                    .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> CreateAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _db.Customers.FindAsync(customerId);
        }


        public async Task<Product?> GetProductAsync(int productId)
        {
            return await _db.Products.FindAsync(productId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                    .ThenInclude(p => p.Seller)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }


        public async Task<bool> CustomerExistsAsync(int customerId) =>
           await _db.Customers.AnyAsync(c => c.CustomerId == customerId);

        public async Task<bool> SellerExistsAsync(int sellerId) =>
            await _db.Sellers.AnyAsync(s => s.SellerId == sellerId);

        

        public async Task<List<Order>> GetOrdersBySellerAsync(int sellerId) =>
            await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                    .ThenInclude(p => p.Seller)
                .Where(o => o.Product.SellerId == sellerId)
                .ToListAsync();

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderUpdateStatusDto dto)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            order.Status = dto.NewStatus;
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
            return true;
        }

    }



}  

