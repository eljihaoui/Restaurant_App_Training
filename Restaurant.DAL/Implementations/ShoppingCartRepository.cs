using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace Restaurant.DAL.Implementations
{
    public class ShoppingCartRepository : Repository<MenuItem>, IShoppingCartRepository
    {
        public RestaurantDBContext _db => _context as RestaurantDBContext;
        public ShoppingCartRepository(RestaurantDBContext context) : base(context)
        {

        }
    }
}
