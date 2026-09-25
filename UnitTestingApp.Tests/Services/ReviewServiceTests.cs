using Moq;
using UnitTestingApp.Models;
using UnitTestingApp.Repositories;
using UnitTestingApp.Services;

namespace UnitTestingApp.Tests.Services
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> _mockRepository;
        private readonly ReviewService _reviewService;

        public ReviewServiceTests()
        {
            _mockRepository = new Mock<IReviewRepository>();
            _reviewService = new ReviewService(_mockRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllReviews()
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

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(reviews);

            // Act
            var result = _reviewService.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(5, result[0].Rating);
        }

        [Fact]
        public void GetById_ShouldReturnReview()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 1,
                ProductId = 1,
                Rating = 5,
                Comment = "Excellent product!"
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(review);

            // Act
            var result = _reviewService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ReviewId);
            Assert.Equal(5, result.Rating);
        }

        [Fact]
        public void Add_ShouldReturnAddedReview()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 2,
                ProductId = 2,
                Rating = 4,
                Comment = "Good product."
            };

            _mockRepository
                .Setup(r => r.Add(review))
                .Returns(review);

            // Act
            var result = _reviewService.Add(review);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ReviewId);
            Assert.Equal("Good product.", result.Comment);
        }

        [Fact]
        public void Update_ShouldReturnTrue_WhenReviewIsUpdated()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 1,
                ProductId = 1,
                Rating = 4,
                Comment = "Updated review."
            };

            _mockRepository
                .Setup(r => r.Update(review))
                .Returns(true);

            // Act
            var result = _reviewService.Update(review);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldReturnTrue_WhenReviewIsDeleted()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.Delete(1))
                .Returns(true);

            // Act
            var result = _reviewService.Delete(1);

            // Assert
            Assert.True(result);
        }
    }
}