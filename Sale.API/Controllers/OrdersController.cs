using Microsoft.AspNetCore.Mvc;
using Sale.BAL;
using Sale.DTOs;
using Sale.Models.DTOs;

namespace Sale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }


        [HttpGet("{id:int}", Name = "GetOrderById")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var order = await _service.CreateOrderAsync(dto);
            if (order == null)
                return BadRequest("Invalid customer, product, or quantity.");

            return CreatedAtRoute("GetOrderById", new { id = order.Id }, order);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto dto)
        {
            var success = await _service.UpdateOrderAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteOrderAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("bycustomer/{customerId:int}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var orders = await _service.GetOrdersByCustomerAsync(customerId);
            if (!orders.Any()) return NotFound("Customer not found or no orders");
            return Ok(orders);
        }

        [HttpGet("byseller/{sellerId:int}")]
        public async Task<IActionResult> GetBySeller(int sellerId)
        {
            var orders = await _service.GetOrdersBySellerAsync(sellerId);
            if (!orders.Any()) return NotFound("Seller not found or no orders");
            return Ok(orders);
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderUpdateStatusDto dto)
        {
            var success = await _service.UpdateOrderStatusAsync(id, dto);
            if (!success)
                return NotFound("Order not found or update failed.");

            return Ok(new { message = "Order status updated successfully." });
        }
    }
}
