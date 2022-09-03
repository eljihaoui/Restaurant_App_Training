using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepo { get; }
        IFoodTypeRepository FoodTypeRepo { get; }   
        IMenuItemRepository MenuItemRepo { get; }   
        IShoppingCartRepository ShoppingCartRepo { get; }   
        Task<bool> Save();
    }
}
