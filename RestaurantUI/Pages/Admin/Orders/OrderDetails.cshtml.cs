using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

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
    }
}
