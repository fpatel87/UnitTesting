using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            return CreatedAtAction(nameof(GetById),
                new { id = result.ProductId }, result);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}