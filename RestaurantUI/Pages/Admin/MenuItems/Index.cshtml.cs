using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.MenuItems
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitofWotk;
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IndexModel(IUnitOfWork unitofWotk)
        {
            _unitofWotk = unitofWotk;
        }
        public async Task OnGet()
        {
            
        }

        public async Task<IActionResult> OnGetList()
        {
            MenuItems = await _unitofWotk.MenuItemRepo.GetAll(filter:null, "Category,FoodType");
            //MenuItems= MenuItems.OrderByDescending(x => x.Name).ToList();   
            return new JsonResult(new { data = MenuItems});
        }

    }
}
