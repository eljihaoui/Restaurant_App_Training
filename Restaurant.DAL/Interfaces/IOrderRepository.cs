using Restaurant.DAL.Implementations;
using Restaurant.Models;

namespace Restaurant.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
        void UpdateStatus(int OrderId, string Status);
    }
}
