using UnitTestingApp.Models;
using UnitTestingApp.Repositories;

namespace UnitTestingApp.Services
{
    public class ReviewService(IReviewRepository reviewRepository) : IReviewService
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;

        public List<Review> GetAll()
        {
            return _reviewRepository.GetAll();
        }

        public Review GetById(int id)
        {
            return _reviewRepository.GetById(id);
        }

        public Review Add(Review review)
        {
            return _reviewRepository.Add(review);
        }

        public bool Update(Review review)
        {
            return _reviewRepository.Update(review);
        }

        public bool Delete(int id)
        {
            return _reviewRepository.Delete(id);
        }
    }
}