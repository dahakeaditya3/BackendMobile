using Microsoft.AspNetCore.Mvc;
using Sale.BAL.Services;
using Sale.Models.DTOs;

namespace Sale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService) => _cartService = cartService;

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemCreateDto dto)
        {
            try
            {
                var saved = await _cartService.AddToCartAsync(dto);
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var items = await _cartService.GetCartAsync(customerId);
            return Ok(items);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem([FromBody] CartItemUpdateDto dto)
        {
            var ok = await _cartService.UpdateItemAsync(dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var ok = await _cartService.RemoveItemAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] BulkOrderCreateDto dto)
        {
            var ok = await _cartService.CheckoutFromCartAsync(dto);
            if (!ok) return BadRequest(new { message = "Checkout failed" });
            return Ok(new { message = "Order placed successfully" });
        }
    }
}
