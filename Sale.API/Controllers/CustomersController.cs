using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sale.BAL.Services;
using Sale.Models.DTOs;
namespace Sale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;
        public CustomersController(CustomerService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        //[Authorize(Roles = "Customer")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            try
            {
                var customer = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "Customer")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto dto)
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

        [Authorize(Roles = "Customer")]
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
    }
}
