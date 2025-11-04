using Microsoft.AspNetCore.Mvc;
using Sale.BAL.Services;
using Sale.Models.DTOs;

namespace Sale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly SellerService _service;
        public SellersController(SellerService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SellerCreateDto dto)
        {
            try
            {
                var seller = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = seller.SellerId }, seller);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SellerUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("byemail")]
        //public async Task<IActionResult> GetByEmail([FromQuery] string email)
        //{
        //    var seller = await _service.GetByEmailAsync(email);
        //    if (seller == null) return NotFound("Seller not found.");

        //    return Ok(new SellerReadDto
        //    {
        //        SellerId = seller.SellerId,
        //        SellerName = seller.SellerName,
        //        StoreName = seller.StoreName,
        //        Email = seller.Email,
        //        Contact = seller.Contact,
        //        State = seller.State,
        //        City = seller.City,
        //        Address = seller.Address,
        //        CreatedOn = seller.CreatedOn
        //    });
        //}
    }
}
