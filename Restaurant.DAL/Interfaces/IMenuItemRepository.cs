using Restaurant.DAL.Implementations;
using Restaurant.Models;

namespace Restaurant.DAL.Interfaces
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem menuItem);
    }
}
