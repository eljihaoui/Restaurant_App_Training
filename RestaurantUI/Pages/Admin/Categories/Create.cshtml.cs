using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace RestaurantUI.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category Categ { get; set; }

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
            Categ = new Category();
        }

        //Post handler 
        public async Task<IActionResult> OnPost()
        {
            if (Categ.DisplayOrder.ToString() == Categ.Name)
            {
                ModelState.AddModelError("customError1", "le nom doit etre <> de dsiplayOrder");
            }
            bool isDigitExists = !string.IsNullOrEmpty(Categ.Name) && Categ.Name.Any(c => char.IsDigit(c));
            if (isDigitExists)
            {
                ModelState.AddModelError("ErrorDigit", "le nom ne doit pas contenir des chiffres");
            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.CategoryRepo.Add(Categ);
                TempData["msg"] = "Category created successfully ";
                TempData["action"] = "create";
                bool res = await _unitOfWork.Save();
                if (res)
                {
                    _notify.AddSuccessToastMessage("Category created successfully");
                    return RedirectToPage("Index"); // if is Valid 
                }
                else
                {
                    _notify.AddErrorToastMessage("Category not created !!!!");
                    return Page();
                }

            }
            return Page(); // if not is valid 
        }

    }
}
