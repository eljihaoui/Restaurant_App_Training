using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Utility;

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
        public async Task<IActionResult> OnGetList(string? status = null)
        {
            OrderList = await _unitofWork.OrderRepo.GetAll(includeProperties: "ApplicationUser");
            switch (status)
            {
                case "cancelled":
                    OrderList = OrderList.Where(o => o.Status == ConstDefs.StatusCancelled
                    || o.Status == ConstDefs.StatusRejected);
                    break;
                case "completed":
                    OrderList = OrderList.Where(o => o.Status == ConstDefs.StatusCompleted);
                    break;
                case "ready":
                    OrderList = OrderList.Where(o => o.Status == ConstDefs.StatusReady);
                    break;
                case "inprocess":
                    OrderList = OrderList.Where(o => o.Status == ConstDefs.StatusInProcess);
                    break;
                default:
                    OrderList = OrderList;
                    break;
            }
            return new JsonResult(new { data = OrderList });
        }
    }
}
