using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Utility;
using Stripe.Checkout;

namespace RestaurantUI.Pages.Customer.Cart
{
    public class OrderConfirmModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWOrk;
        public int OrderId { get; set; }
        public OrderConfirmModel(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }
        public async Task OnGet(int id )
        {
            Order order = await _unitOfWOrk.OrderRepo.GetById(o => o.Id == id);
            if (order.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(order.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    order.Status=ConstDefs.StatusSubmitted; 
                    order.PaymentIntentId=session.PaymentIntentId;

                    //remove shopping cart
                    List<ShoppingCart> list = (await _unitOfWOrk.ShoppingCartRepo.GetAll(
                        filter : u=>u.UserId==order.UserId)).ToList();
                    _unitOfWOrk.ShoppingCartRepo.RemoveRange(list);
                    await _unitOfWOrk.Save();
                    OrderId = id;
                }

            }
        }
    }
}
