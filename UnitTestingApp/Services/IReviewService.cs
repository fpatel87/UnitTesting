using UnitTestingApp.Models;

namespace UnitTestingApp.Services
{
    public interface IReviewService
    {
        List<Review> GetAll();
        Review GetById(int id);
        Review Add(Review review);
        bool Update(Review review);
        bool Delete(int id);
    }
}