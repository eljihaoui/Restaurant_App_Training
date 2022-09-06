using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models.ViewModels;
using Restaurant.Models;
using Restaurant.Utility;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantUI.Pages.Admin.Orders
{
    [Authorize(Roles= $"{ConstDefs.ManagerRole},{ConstDefs.KitchenRole}")]
    public class OrderManagerModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailsViewModel> OrderDetailsViewModelList { get; set; }
        public OrderManagerModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderDetailsViewModelList = new List<OrderDetailsViewModel>();
        }
        public async Task OnGet()
        {
            List<Order> orders = (await _unitOfWork.OrderRepo.GetAll(
                filter: o => o.Status == ConstDefs.StatusSubmitted
                || o.Status == ConstDefs.StatusInProcess)).ToList();
            foreach (Order ord in orders)
            {
                OrderDetailsViewModel ordVM = new()
                {
                    Order = ord,
                    OrderDetails = await _unitOfWork.OrderDetailsRepo.GetAll(o => o.OrderId == ord.Id)
                };
                OrderDetailsViewModelList.Add(ordVM);
            }
        }
        public async Task<IActionResult> OnPostOrderStart(int OrderId)
        {
            _unitOfWork.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusInProcess);
            await _unitOfWork.Save();
            return RedirectToPage("OrderManager");
        }
        public async Task<IActionResult> OnPostOrderReady(int OrderId)
        {
            _unitOfWork.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusReady);
            await _unitOfWork.Save();
            return RedirectToPage("OrderManager");
        }
        public async Task<IActionResult> OnPostOrderCancel(int OrderId)
        {
            _unitOfWork.OrderRepo.UpdateStatus(OrderId, ConstDefs.StatusCancelled);
            await _unitOfWork.Save();
            return RedirectToPage("OrderManager");
        }
    }
}
