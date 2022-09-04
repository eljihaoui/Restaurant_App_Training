using Restaurant.DAL.Implementations;
using Restaurant.Models;

namespace Restaurant.DAL.Interfaces
{
    public interface IOrderDetailsCartRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}
