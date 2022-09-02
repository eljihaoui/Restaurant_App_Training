using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FoodType foodType { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _notify;

        public EditModel(IUnitOfWork unitOfWork, IToastNotification notify)
        {
            _unitOfWork= unitOfWork;
            _notify = notify;
        }
        // get handler 
        public async Task OnGet(int id)
        {
            foodType = await _unitOfWork.FoodTypeRepo.GetById(f=>f.FoodTypeId==id);
            //Categ = _context.Category.FirstOrDefault(c => c.Id == id);
            //Categ = _context.Category.SingleOrDefault(c => c.Id == id);
            //Categ = _context.Category.Where(c => c.Id == id).FirstOrDefault();
        }

        //Post handler
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodTypeRepo.Update(foodType);
                 bool res = await _unitOfWork.Save();
                if (res)
                {
                    TempData["msg"] = "Food Type updated successfully ";
                    TempData["action"] = "update";
                    _notify.AddSuccessToastMessage("Food Type updated successfully");
                    return RedirectToPage("Index");
                }
                else
                {
                    _notify.AddErrorToastMessage("Food Type updated successfully");
                    return Page();
                }


            }
            return Page();
        }

    }
}
