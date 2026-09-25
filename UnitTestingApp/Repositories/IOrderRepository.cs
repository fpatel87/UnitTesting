using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Add(Order order);
        bool Update(Order order);
        bool Delete(int id);
    }
}