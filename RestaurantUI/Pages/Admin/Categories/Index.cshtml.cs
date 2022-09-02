using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitofWotk;
        public IEnumerable<Category> Categories { get; set; }
        public IndexModel(IUnitOfWork unitofWotk)
        {
            _unitofWotk = unitofWotk;
        }
        public  async Task OnGet()
        {
            Categories = await _unitofWotk.CategoryRepo.GetAll();
        }
    }
}
