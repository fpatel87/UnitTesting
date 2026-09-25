using UnitTestingApp.Models;
using UnitTestingApp.Repositories;

namespace UnitTestingApp.Services
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;


        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }

        public bool Update(Order order)
        {
            return _orderRepository.Update(order);
        }

        public bool Delete(int id)
        {
            return _orderRepository.Delete(id);
        }
    }
}