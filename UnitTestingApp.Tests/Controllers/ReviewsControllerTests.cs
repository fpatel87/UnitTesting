using Moq;
using Microsoft.AspNetCore.Mvc;
using UnitTestingApp.Controllers;
using UnitTestingApp.Models;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Controllers
{
    public class ReviewsControllerTests
    {
        private readonly Mock<IReviewService> _mockService;
        private readonly ReviewsController _controller;

        public ReviewsControllerTests()
        {
            _mockService = new Mock<IReviewService>();
            _controller = new ReviewsController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new()
                {
                    ReviewId = 1,
                    ProductId = 1,
                    Rating = 5,
                    Comment = "Excellent product!"
                },
                new()
                {
                    ReviewId = 2,
                    ProductId = 2,
                    Rating = 4,
                    Comment = "Good product."
                }
            };

            _mockService
                .Setup(s => s.GetAll())
                .Returns(reviews);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReviews = Assert.IsType<List<Review>>(okResult.Value);

            Assert.Equal(2, returnedReviews.Count);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenReviewExists()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 1,
                ProductId = 1,
                Rating = 5,
                Comment = "Excellent product!"
            };

            _mockService
                .Setup(s => s.GetById(1))
                .Returns(review);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReview = Assert.IsType<Review>(okResult.Value);

            Assert.Equal(1, returnedReview.ReviewId);
            Assert.Equal(5, returnedReview.Rating);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenReviewDoesNotExist()
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
            var review = new Review
            {
                ReviewId = 2,
                ProductId = 2,
                Rating = 4,
                Comment = "Good product."
            };

            _mockService
                .Setup(s => s.Add(review))
                .Returns(review);

            // Act
            var result = _controller.Add(review);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(ReviewsController.GetById),
                createdResult.ActionName);

            Assert.Equal(review, createdResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnOk_WhenReviewIsUpdated()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 1,
                ProductId = 1,
                Rating = 4,
                Comment = "Updated review."
            };

            _mockService
                .Setup(s => s.Update(review))
                .Returns(true);

            // Act
            var result = _controller.Update(review);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFound_WhenReviewDoesNotExist()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 999,
                ProductId = 1,
                Rating = 3,
                Comment = "Unknown review."
            };

            _mockService
                .Setup(s => s.Update(review))
                .Returns(false);

            // Act
            var result = _controller.Update(review);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenReviewIsDeleted()
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
        public void Delete_ShouldReturnNotFound_WhenReviewDoesNotExist()
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