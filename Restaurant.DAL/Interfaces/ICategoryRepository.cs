using Restaurant.DAL.Implementations;
using Restaurant.Models;

namespace Restaurant.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
