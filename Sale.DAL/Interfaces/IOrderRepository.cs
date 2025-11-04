
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.DAL
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> CreateAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<List<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<List<Order>> GetOrdersBySellerAsync(int sellerId);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<bool> SellerExistsAsync(int sellerId);
        Task<Customer?> GetCustomerAsync(int customerId);
        Task<Product?> GetProductAsync(int productId);

        Task<bool> UpdateOrderStatusAsync(int orderId, OrderUpdateStatusDto dto);
    }
}
