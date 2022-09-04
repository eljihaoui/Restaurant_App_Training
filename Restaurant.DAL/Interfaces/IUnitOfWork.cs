namespace Restaurant.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepo { get; }
        IFoodTypeRepository FoodTypeRepo { get; }   
        IMenuItemRepository MenuItemRepo { get; }   
        IShoppingCartRepository ShoppingCartRepo { get; }   
        IOrderRepository OrderRepo { get; }
        IOrderDetailsCartRepository OrderDetailsRepo { get; }   

        Task<bool> Save();
    }
}
