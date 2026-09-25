using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly List<Review> _reviews = [
            new()
            {
                ReviewId = 1,
                ProductId = 1,
                Rating = 5,
                Comment = "Excellent product!"
            }
        ];


        public List<Review> GetAll()
        {
            return _reviews;
        }

        public Review GetById(int id)
        {
            return _reviews.FirstOrDefault(r => r.ReviewId == id);
        }

        public Review Add(Review review)
        {
            review.ReviewId = _reviews.Count + 1;
            _reviews.Add(review);
            return review;
        }

        public bool Update(Review review)
        {
            var existing = GetById(review.ReviewId);

            if (existing == null)
                return false;

            existing.ProductId = review.ProductId;
            existing.Rating = review.Rating;
            existing.Comment = review.Comment;

            return true;
        }

        public bool Delete(int id)
        {
            var review = GetById(id);

            if (review == null)
                return false;

            _reviews.Remove(review);
            return true;
        }
    }
}