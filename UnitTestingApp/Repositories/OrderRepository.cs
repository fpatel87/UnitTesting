using UnitTestingApp.Models;

namespace UnitTestingApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = [
        
            new()
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                TotalAmount = 2000
            }
        ];

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.OrderId == id);
        }

        public Order Add(Order order)
        {
            order.OrderId = _orders.Count + 1;
            _orders.Add(order);
            return order;
        }

        public bool Update(Order order)
        {
            var existing = GetById(order.OrderId);

            if (existing == null)
                return false;

            existing.ProductId = order.ProductId;
            existing.Quantity = order.Quantity;
            existing.TotalAmount = order.TotalAmount;

            return true;
        }

        public bool Delete(int id)
        {
            var order = GetById(id);

            if (order == null)
                return false;

            _orders.Remove(order);
            return true;
        }
    }
}