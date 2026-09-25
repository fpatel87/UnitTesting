using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        Review GetById(int id);
        Review Add(Review review);
        bool Update(Review review);
        bool Delete(int id);
    }
}