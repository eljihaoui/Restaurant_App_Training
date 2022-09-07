using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
using Restaurant.Utility;
using Stripe;

namespace RestaurantUI.Pages.Admin.Orders
{

    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWotk;
        public OrderDetailsViewModel OrderDetailsVM { get; set; }

        public OrderDetailsModel(IUnitOfWork unitOfWotk)
        {
            _unitOfWotk = unitOfWotk;
        }
        public async Task OnGet(int id)
        {
            OrderDetailsVM = new()
            {
                Order = await _unitOfWotk.OrderRepo.GetById(u => u.Id == id, includeProperties:"ApplicationUser"),
                OrderDetails = await _unitOfWotk.OrderDetailsRepo.GetAll(o => o.OrderId == id)
            };
        }
        public async Task<IActionResult> OnPostOrderConplete(int OrderId)
        {
            _unitOfWotk.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusCompleted);
            await _unitOfWotk.Save();
            return RedirectToPage("OrderList");
        }
        public async Task<IActionResult> OnPostOrderCancel(int OrderId)
        {
            _unitOfWotk.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusCancelled);
            await _unitOfWotk.Save();
            return RedirectToPage("OrderList");
        }

        public async Task<IActionResult> OnPostRefundOrder(int OrderId)
        {
           Order order = await _unitOfWotk.OrderRepo.GetById(o=>o.Id == OrderId);
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = order.PaymentIntentId
            };
            var service = new RefundService();
            Refund refund= service.Create(options);
            _unitOfWotk.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusRefunded);
            await _unitOfWotk.Save();
            return RedirectToPage("OrderList");

        }
    }
}
