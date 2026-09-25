using Moq;
using UnitTestingApp.Models;
using UnitTestingApp.Repositories;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllOrders()
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

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(orders);

            // Act
            var result = _orderService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].OrderId);
        }

        [Fact]
        public void GetById_ShouldReturnOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                TotalAmount = 2000
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(order);

            // Act
            var result = _orderService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal(2, result.Quantity);
        }

        [Fact]
        public void Add_ShouldReturnAddedOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 2,
                ProductId = 2,
                Quantity = 1,
                TotalAmount = 600
            };

            _mockRepository
                .Setup(r => r.Add(order))
                .Returns(order);

            // Act
            var result = _orderService.Add(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.OrderId);
            Assert.Equal(600, result.TotalAmount);
        }

        [Fact]
        public void Update_ShouldReturnTrue_WhenOrderIsUpdated()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 3,
                TotalAmount = 3000
            };

            _mockRepository
                .Setup(r => r.Update(order))
                .Returns(true);

            // Act
            var result = _orderService.Update(order);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldReturnTrue_WhenOrderIsDeleted()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.Delete(1))
                .Returns(true);

            // Act
            var result = _orderService.Delete(1);

            // Assert
            Assert.True(result);
        }
    }
}