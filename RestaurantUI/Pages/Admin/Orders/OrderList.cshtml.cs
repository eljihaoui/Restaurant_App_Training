using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.Orders
{
 
    public class OrderListModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        public IEnumerable<Order> OrderList { get; set; }
        public OrderListModel(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnGetList()
        {
            OrderList =await _unitofWork.OrderRepo.GetAll(includeProperties: "ApplicationUser");
            return new JsonResult(new { data = OrderList });
       }
    }
}
