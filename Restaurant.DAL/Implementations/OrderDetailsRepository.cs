using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace Restaurant.DAL.Implementations
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsCartRepository
    {
        public RestaurantDBContext _db => _context as RestaurantDBContext;
        public OrderDetailsRepository(RestaurantDBContext context) : base(context)
        {

        }

        public void Update(OrderDetails orderDetails)
        {
            _db.OrderDetails.Update(orderDetails);  
        }
    }
}
