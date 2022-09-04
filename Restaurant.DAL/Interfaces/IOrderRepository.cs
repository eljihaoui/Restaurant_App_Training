using Restaurant.DAL.Implementations;
using Restaurant.Models;

namespace Restaurant.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
    }
}
