using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace Restaurant.DAL.Implementations
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        public RestaurantDBContext _db => _context as RestaurantDBContext;
        public MenuItemRepository(RestaurantDBContext context) : base(context)
        {

        }
     
        public void Update(MenuItem menuItem)
        {
            MenuItem vMenuItem = _db.MenuItem.Find(menuItem.Id);
            if (vMenuItem != null)
            {
                vMenuItem.Name = menuItem.Name;
                vMenuItem.Description = menuItem.Description;
                vMenuItem.Price = menuItem.Price;
                vMenuItem.CategoryId = menuItem.CategoryId;
                vMenuItem.FoodTypeId = menuItem.FoodTypeId;
                if (menuItem.ImageUrl != null)
                {
                    vMenuItem.ImageUrl = menuItem.ImageUrl; 
                }
            }
        }
    }
}
