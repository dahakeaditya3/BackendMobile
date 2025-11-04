
using Sale.DAL;
using Sale.Dtos;
using Sale.DTOs;
using Sale.Models.Dtos;
using Sale.Models.DTOs;
using Sale.Models.Entities;

namespace Sale.BAL
{
    public class OrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository repo) => _repo = repo;
        private string? ConvertImage(byte[]? img) => img != null ? Convert.ToBase64String(img) : null;

        public async Task<List<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _repo.GetAllOrdersAsync();
            return orders.Select(o => new OrderReadDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.CustomerName,
                ProductId = o.ProductId,
                ProductName = o.Product.ProductName,
                Quantity = o.Quantity,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedOn = o.CreatedOn,
                DeliveryDate = o.DeliveryDate
            }).ToList();
        }


        public async Task<OrderReadDto?> CreateOrderAsync(OrderCreateDto dto)
        {
            var customer = await _repo.GetCustomerAsync(dto.CustomerId);
            if (customer == null) return null;

            var product = await _repo.GetProductAsync(dto.ProductId);
            if (product == null) return null;

            if (dto.Quantity <= 0) return null;

            var total = dto.Quantity * product.Price;

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                ReceiverName = dto.ReceiverName,    
                ReceiverContactNumber = dto.ReceiverContactNumber,
                OrderAddress = dto.OrderAddress,
                PostalCode = dto.PostalCode,
                TotalPrice = total,
                Status = dto.Status= "Pending",
                CreatedOn = DateTime.UtcNow,
                DeliveryDate=DateTime.UtcNow.AddDays(7)
            };

            var saved = await _repo.CreateAsync(order);
            if (saved == null) return null;

            return new OrderReadDto
            {
                Id = saved.Id,
                CustomerId = saved.CustomerId,
                ProductId = saved.ProductId,
                Quantity = saved.Quantity,
                ReceiverName = saved.ReceiverName,
                ReceiverContactNumber=saved.ReceiverContactNumber,
                OrderAddress=saved.OrderAddress,
                PostalCode=saved.PostalCode,
                TotalPrice = saved.TotalPrice,
                Status = saved.Status,
                CreatedOn = saved.CreatedOn,
                DeliveryDate=saved.DeliveryDate,
            };
        }

        public async Task<OrderReadDto?> GetOrderByIdAsync(int id)
        {
            var o = await _repo.GetByIdAsync(id);
            if (o == null) return null;

            return new OrderReadDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.CustomerName,
                ProductId = o.ProductId,
                ProductName = o.Product.ProductName,
                Quantity = o.Quantity,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedOn = o.CreatedOn,
                DeliveryDate=o.DeliveryDate
            };
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderUpdateDto dto)
        {
            var order = await _repo.GetOrderByIdAsync(id);
            if (order == null) return false;

            if (dto.Quantity.HasValue && dto.Quantity > 0)
            {
                order.Quantity = dto.Quantity.Value;
                order.TotalPrice = order.Quantity * order.Product.Price;
            }

            await _repo.UpdateOrderAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _repo.GetOrderByIdAsync(id);
            if (order == null) return false;

            await _repo.DeleteOrderAsync(order);
            return true;
        }

        public async Task<List<OrderForCustomerDto>> GetOrdersByCustomerAsync(int customerId)
        {
            if (!await _repo.CustomerExistsAsync(customerId))
                return new List<OrderForCustomerDto>();

            var orders = await _repo.GetOrdersByCustomerAsync(customerId);

            return orders.Select(o => new OrderForCustomerDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.CustomerName,
                ProductId = o.ProductId,
                ProductName = o.Product.ProductName,
                ProductCompany = o.Product.ProductCompany,
                ProductPrice = o.Product.Price,
                ProductDescription = o.Product.Description,
                Images = new ProductImagesDto
                {
                    Image1 = ConvertImage(o.Product.Image1),
                    Image2 = ConvertImage(o.Product.Image2),
                    Image3 = ConvertImage(o.Product.Image3),
                    Image4 = ConvertImage(o.Product.Image4)
                },
                Seller = new SellerDto
                {
                    SellerName = o.Product.Seller.SellerName,
                    StoreName = o.Product.Seller.StoreName,
                    Contact = o.Product.Seller.Contact,
                    Address = o.Product.Seller.Address
                },
                ReceiverName=o.ReceiverName,
                Quantity = o.Quantity,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedOn = o.CreatedOn,
                DeliveryDate = o.DeliveryDate,
            }).ToList();
        }


        public async Task<List<OrderForSellerDto>> GetOrdersBySellerAsync(int sellerId)
        {
            if (!await _repo.SellerExistsAsync(sellerId)) return new List<OrderForSellerDto>();

            var orders = await _repo.GetOrdersBySellerAsync(sellerId);

            return orders.Select(o => new OrderForSellerDto
            {
                Id = o.Id,
                Customer = new CustomerDto
                {
                    CustomerId = o.CustomerId,
                    Name = o.Customer.CustomerName,
                    Contact = o.Customer.Contact,
                    Gender = o.Customer.Gender,
                    City = o.Customer.City,
                    Email = o.Customer.Email
                },
                Product = new ProductForSellerDto
                {
                    ProductId = o.ProductId,
                    Name = o.Product.ProductName,
                    Company = o.Product.ProductCompany,
                    Price = o.Product.Price,
                    Images = new ProductImagesDto
                    {
                        Image1 = ConvertImage(o.Product.Image1),
                        Image2 = ConvertImage(o.Product.Image2),
                        Image3 = ConvertImage(o.Product.Image3),
                        Image4 = ConvertImage(o.Product.Image4)
                    },
                    Seller = new SellerInfoDto
                    {
                        SellerId = o.Product.SellerId,
                        SellerName = o.Product.Seller.SellerName,
                        StoreName = o.Product.Seller.StoreName,
                        Phone = o.Product.Seller.Contact,
                        Address = o.Product.Seller.Address
                    }
                },
                ReceiverName=o.ReceiverName,
                ReceiverContactNumber = o.ReceiverContactNumber,
                OrderAddress = o.OrderAddress,
                PostalCode=o.PostalCode,
                Quantity = o.Quantity,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedOn = o.CreatedOn,
                DeliveryDate = o.DeliveryDate,
            }).ToList();
        }

        public async Task<bool> CreateOrdersFromCartAsync(int customerId, List<CartItem> cartItems)
        {
            foreach (var ci in cartItems)
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ReceiverName = "", 
                    ReceiverContactNumber = "",
                    OrderAddress = "",
                    PostalCode = "",
                    Status= "Pending",
                    TotalPrice = ci.UnitPrice * ci.Quantity,
                    CreatedOn = DateTime.UtcNow,
                    DeliveryDate=DateTime.UtcNow.AddDays(7)
                };


                var saved = await _repo.CreateAsync(order);
                if (saved == null) return false;
            }
            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderUpdateStatusDto dto)
        {
            return await _repo.UpdateOrderStatusAsync(orderId, dto);
        }
    }
}
