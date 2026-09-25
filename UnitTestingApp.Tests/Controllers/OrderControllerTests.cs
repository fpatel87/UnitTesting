using Moq;
using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Controllers;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockService = new Mock<IOrderService>();
            _controller = new OrderController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            // Arrange
            var orders = new List<Order>
            {
                new()
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    TotalAmount = 2000
                },
                new() 
                {
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 1,
                    TotalAmount = 600
                }
            };

            _mockService
                .Setup(s => s.GetAll())
                .Returns(orders);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrders = Assert.IsType<List<Order>>(okResult.Value);

            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenOrderExists()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                TotalAmount = 2000
            };

            _mockService
                .Setup(s => s.GetById(1))
                .Returns(order);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);

            Assert.Equal(1, returnedOrder.OrderId);
            Assert.Equal(2, returnedOrder.Quantity);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetById(999))
                .Returns(() => null);
               

            // Act
            var result = _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 2,
                ProductId = 2,
                Quantity = 1,
                TotalAmount = 600
            };

            _mockService
                .Setup(s => s.Add(order))
                .Returns(order);

            // Act
            var result = _controller.Add(order);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(OrderController.GetById),
                createdResult.ActionName);

            Assert.Equal(order, createdResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnOk_WhenOrderIsUpdated()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 3,
                TotalAmount = 3000
            };

            _mockService
                .Setup(s => s.Update(order))
                .Returns(true);

            // Act
            var result = _controller.Update(order);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 999,
                ProductId = 1,
                Quantity = 1,
                TotalAmount = 100
            };

            _mockService
                .Setup(s => s.Update(order))
                .Returns(false);

            // Act
            var result = _controller.Update(order);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenOrderIsDeleted()
        {
            // Arrange
            _mockService
                .Setup(s => s.Delete(1))
                .Returns(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            _mockService
                .Setup(s => s.Delete(999))
                .Returns(false);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}