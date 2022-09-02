using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public FoodType foodType { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _notify;
        public CreateModel(IUnitOfWork unitOfWork, IToastNotification notify)
        {
            _unitOfWork = unitOfWork;
            _notify = notify;
        }
        // get handler 
        public void OnGet()
        {
            foodType = new FoodType();
        }

        //Post handler 
        public async Task<IActionResult> OnPost()
        {
            bool isDigitExists = !string.IsNullOrEmpty(foodType.FoodTypeName) && foodType.FoodTypeName.Any(c => char.IsDigit(c));
            if (isDigitExists)
            {
                ModelState.AddModelError("ErrorDigit", "le nom ne doit pas contenir des chiffres");
            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.FoodTypeRepo.Add(foodType);
                TempData["msg"] = "Food type created successfully ";
                TempData["action"] = "create";
                bool res = await _unitOfWork.Save();
                if (res)
                {
                    _notify.AddSuccessToastMessage("Food type  created successfully");
                    return RedirectToPage("Index"); // if is Valid 
                }
                else
                {
                    _notify.AddErrorToastMessage("Food type  not created !!!!");
                    return Page();
                }

            }
            return Page(); // if not is valid 
        }

    }
}
