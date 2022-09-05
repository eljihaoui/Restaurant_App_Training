using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Utility;
using System.Security.Claims;

namespace RestaurantUI.Pages.Customer.Cart
{
    [Authorize]
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Order Order { get; set; }
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            Order = new Order();
        }
        public async Task OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                ShoppingCartList = await _unitOfWork.ShoppingCartRepo.GetAll(
                    filter: u => u.UserId == userId,
                    includeProperties: "ApplicationUser,MenuItem"
                    );
                foreach (var cart in ShoppingCartList)
                {
                    Order.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }
                ApplicationUser AppUser = ShoppingCartList.First().ApplicationUser;
                Order.PickUpName = AppUser.FirstName + " " + AppUser.LastName;
                Order.PhoneNumber = AppUser.PhoneNumber;
                Order.OrderDate = DateTime.Now;
            }
        }

        // action PlaceOrder
        public async Task OnPost()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                ShoppingCartList = await _unitOfWork.ShoppingCartRepo.GetAll(
                    filter: u => u.UserId == userId,
                    includeProperties: "ApplicationUser,MenuItem"
                    );
                foreach (var cart in ShoppingCartList)
                {
                    Order.OrderTotal += (cart.MenuItem.Price * cart.Count);
                }

                // add Order
                Order.Status = ConstDefs.StatusPending;
                Order.OrderDate = DateTime.Now;
                Order.UserId = userId;
                Order.PickUpTime = Convert.ToDateTime(
                    Order.PickUpDate.ToShortDateString() + " " + Order.PickUpTime.ToShortTimeString());
                await _unitOfWork.OrderRepo.Add(Order);
                await _unitOfWork.Save();

                // Add order Deatils
                foreach (ShoppingCart item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = Order.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    await _unitOfWork.OrderDetailsRepo.Add(orderDetails);
                }
                _unitOfWork.ShoppingCartRepo.RemoveRange(ShoppingCartList);
                await _unitOfWork.Save();   



            }
        }
    }
}
