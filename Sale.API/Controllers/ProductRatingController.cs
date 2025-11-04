using Microsoft.AspNetCore.Mvc;
using Sale.BAL.Services;
using Sale.Dtos;

namespace Sale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        private readonly ProductRatingService _ratingService;

        public ProductRatingController(ProductRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRating([FromBody] ProductRatingDto dto)
        {
            await _ratingService.AddRatingAsync(dto);
            return Ok(new { message = "Rating added successfully!" });
        }

        [HttpGet("average/{productId}")]
        public async Task<IActionResult> GetAverageRating(int productId)
        {
            var avg = await _ratingService.GetAverageRatingAsync(productId);
            return Ok(avg);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetRatings(int productId)
        {
            var ratings = await _ratingService.GetRatingsAsync(productId);
            return Ok(ratings);
        }
    }
}
