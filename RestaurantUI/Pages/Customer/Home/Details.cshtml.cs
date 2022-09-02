using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantUI.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        public IUnitOfWork _unitOfWork { get; }
        public MenuItem MenuItem { get; set; }

        [Range(1, 100, ErrorMessage = "Count must be less than 100")]
        [Required(ErrorMessage = "Count is required !!!")]
        public int Count { get; set; }
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OnGet(int? id)
        {
            MenuItem = await _unitOfWork.MenuItemRepo.GetById
                             (m => m.Id == id, "Category,FoodType");
        }
    }
}
