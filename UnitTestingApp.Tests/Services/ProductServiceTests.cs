using Moq;
using UnitTestingApp.Models;
using UnitTestingApp.Repositories;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllProducts()
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
                    
                    Name = "Phone", Price = 600 
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(products);

            // Act
            var result = _productService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Laptop", result[0].Name);
        }

        [Fact]
        public void GetById_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Laptop",
                Price = 1000
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(product);

            // Act
            var result = _productService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Laptop", result.Name);
        }

        [Fact]
        public void Add_ShouldReturnAddedProduct()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 3,
                Name = "Tablet",
                Price = 500
            };

            _mockRepository
                .Setup(r => r.Add(product))
                .Returns(product);

            // Act
            var result = _productService.Add(product);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tablet", result.Name);
            Assert.Equal(500, result.Price);
        }

        [Fact]
        public void Update_ShouldReturnTrue_WhenProductIsUpdated()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Updated Laptop",
                Price = 1200
            };

            _mockRepository
                .Setup(r => r.Update(product))
                .Returns(true);

            // Act
            var result = _productService.Update(product);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldReturnTrue_WhenProductIsDeleted()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.Delete(1))
                .Returns(true);

            // Act
            var result = _productService.Delete(1);

            // Assert
            Assert.True(result);
        }
    }
}