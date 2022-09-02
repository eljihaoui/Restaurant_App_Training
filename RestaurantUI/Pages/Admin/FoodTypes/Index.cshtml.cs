using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitofWotk;
        public IEnumerable<FoodType> foodTypes { get; set; }
        public IndexModel(IUnitOfWork unitofWotk)
        {
            _unitofWotk = unitofWotk;
        }
        public  async Task OnGet()
        {
            foodTypes = await _unitofWotk.FoodTypeRepo.GetAll();
        }
    }
}
