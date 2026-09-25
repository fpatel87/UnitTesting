using UnitTestingApp.Models;

namespace UnitTestingApp.Services
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Add(Order order);
        bool Update(Order order);
        bool Delete(int id);
    }
}