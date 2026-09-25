using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            var result = _orderService.Add(order);

            return CreatedAtAction(nameof(GetById),
                new { id = result.OrderId }, result);
        }

        [HttpPut]
        public IActionResult Update(Order order)
        {
            var result = _orderService.Update(order);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _orderService.Delete(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}