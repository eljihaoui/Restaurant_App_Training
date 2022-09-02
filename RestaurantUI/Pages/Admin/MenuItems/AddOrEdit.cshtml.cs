using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Models.ViewModels;
using IoFile = System.IO.File;

namespace RestaurantUI.Pages.Admin.MenuItems
{
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public MenuItemViewModel MenuItem { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _notify;
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _host;
        public AddOrEditModel(IUnitOfWork unitOfWork, IToastNotification notify, IMapper mapper, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _notify = notify;
            _mapper = mapper;
            _host = host;
            MenuItem = new MenuItemViewModel();
        }
        // get handler 
        public async Task OnGet(int? id)
        {
            if (id != null)
            {
                MenuItem mItem = await _unitOfWork.MenuItemRepo.GetById(m => m.Id == id);
                MenuItem = _mapper.Map<MenuItemViewModel>(mItem);
            }
            CategoryList = (await _unitOfWork.CategoryRepo.GetAll()).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            FoodTypeList = (await _unitOfWork.FoodTypeRepo.GetAll()).Select(c => new SelectListItem()
            {
                Text = c.FoodTypeName,
                Value = c.FoodTypeId.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            string webroot = _host.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var uploads = Path.Combine(webroot, @"Images\MenuItems");

            // Create operation
            if (MenuItem.Id == 0)
            {

                if (ModelState.IsValid)
                {
                    string fileName_img = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(files[0].FileName);
                    var CompleteFileName = Path.Combine(uploads, fileName_img + extention);
                    using (var fileStream = new FileStream(CompleteFileName, FileMode.Create))
                    {
                        files[0].CopyToAsync(fileStream);
                    }
                    MenuItem mItem = _mapper.Map<MenuItem>(MenuItem); // transform viewModel to Model
                    mItem.ImageUrl = @"\Images\MenuItems\" + fileName_img + extention;
                    await _unitOfWork.MenuItemRepo.Add(mItem);
                    bool res = await _unitOfWork.Save();
                    if (res)
                    {
                        _notify.AddSuccessToastMessage("Menu Item Created suuccufully ");
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        _notify.AddErrorToastMessage("Menu Item Not Created suuccufully !!!!");
                    }
                }
                else
                {
                    return Page();
                }

            } // id <> 0 => update Operation
            else
            {
                MenuItem mItemToUpdate = await _unitOfWork.MenuItemRepo.GetById(m => m.Id == MenuItem.Id);
                string ImageUrl = mItemToUpdate.ImageUrl;
                mItemToUpdate = _mapper.Map<MenuItem>(MenuItem);
                if (files.Count > 0)
                {
                    string fileName_img = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(files[0].FileName);
                    var CompleteFileName = Path.Combine(uploads, fileName_img + extention);
                    //delete the old image
                    if (ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webroot, ImageUrl.TrimStart('\\'));
                        if (IoFile.Exists(oldImagePath))
                        {
                            IoFile.Delete(oldImagePath);
                        }
                    }

                    // new upload
                    using (var fileStream = new FileStream(CompleteFileName, FileMode.Create))
                    {
                        files[0].CopyToAsync(fileStream);
                    }
                    mItemToUpdate.ImageUrl = @"\Images\MenuItems\" + fileName_img + extention;
                }
                _unitOfWork.MenuItemRepo.Update(mItemToUpdate);
                if (await _unitOfWork.Save())
                {
                    _notify.AddSuccessToastMessage("MenuItem updated successfully ");
                    return RedirectToPage("Index");
                }
                else
                {
                    _notify.AddWarningToastMessage("MenuItem not updated !!!!! ");
                }
            }
            return Page();
        }

        // On + http method + Named Handler
        // ?Handler=Delete&id=2
        public async Task<IActionResult> OnGetDelete(int? id)
        {
            if (id != null)
            {
                var mItem = await _unitOfWork.MenuItemRepo.GetById(m => m.Id == id);
                _unitOfWork.MenuItemRepo.Remove(mItem);
                bool res = await _unitOfWork.Save();
                if (res)
                {
                    string webroot = _host.WebRootPath;
                    // delete MenuItem Image

                    if (mItem.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webroot, mItem.ImageUrl.TrimStart('\\'));
                        if (IoFile.Exists(oldImagePath))
                        {
                            IoFile.Delete(oldImagePath);
                        }
                    }

                    return new JsonResult(new { success = true, message = "MenuItem deleted successfully" });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "MenuItem not deleted !!!!!" });
                }
            }
            else
            {
                return new JsonResult(new { success = false, message = "MenuItem not deleted !!!!!" });
            }
        }

    }
}
