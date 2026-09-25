using Moq;
using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Controllers;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            // Arrange
            var products = new List<Product>
            {
                new() 
                { 
                    
                    ProductId = 1, 
                    Name = "Laptop", 
                    Price = 1000 
                },
                new() 
                { 
                    ProductId = 2, 
                    Name = "Phone", 
                    Price = 600 
                }
            };

            _mockService
                .Setup(s => s.GetAll())
                .Returns(products);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);

            Assert.Equal(2, returnedProducts.Count);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Laptop",
                Price = 1000
            };

            _mockService
                .Setup(s => s.GetById(1))
                .Returns(product);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(1, returnedProduct.ProductId);
            Assert.Equal("Laptop", returnedProduct.Name);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
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
            var product = new Product
            {
                ProductId = 3,
                Name = "Tablet",
                Price = 500
            };

            _mockService
                .Setup(s => s.Add(product))
                .Returns(product);

            // Act
            var result = _controller.Add(product);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(ProductController.GetById),
                createdResult.ActionName);

            Assert.Equal(product, createdResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnOk_WhenProductIsUpdated()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Updated Laptop",
                Price = 1200
            };

            _mockService
                .Setup(s => s.Update(product))
                .Returns(true);

            // Act
            var result = _controller.Update(product);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 999,
                Name = "Unknown",
                Price = 100
            };

            _mockService
                .Setup(s => s.Update(product))
                .Returns(false);

            // Act
            var result = _controller.Update(product);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenProductIsDeleted()
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
        public void Delete_ShouldReturnNotFound_WhenProductDoesNotExist()
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