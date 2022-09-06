using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace Restaurant.DAL.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public RestaurantDBContext _db => _context as RestaurantDBContext;
        public OrderRepository(RestaurantDBContext context) : base(context)
        {

        }

        public void Update(Order order)
        {
            _db.Order.Update(order);
        }

        public void UpdateStatus(int OrderId, string Status)
        {
            var order = _db.Order.Find(OrderId);    
            if(order != null)
            {
                order.Status = Status;  
            }
        }
    }
}
