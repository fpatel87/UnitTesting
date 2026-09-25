using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

      
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_reviewService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _reviewService.GetById(id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public IActionResult Add(Review review)
        {
            var result = _reviewService.Add(review);

            return CreatedAtAction(nameof(GetById),
                new { id = result.ReviewId }, result);
        }

        [HttpPut]
        public IActionResult Update(Review review)
        {
            var result = _reviewService.Update(review);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _reviewService.Delete(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}