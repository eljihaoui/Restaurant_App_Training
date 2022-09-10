using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Utility;

namespace Restaurant.DAL.DbInitilizer
{
    public class DbInializer : IDbInializer
    {
        private readonly RestaurantDBContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInializer(RestaurantDBContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this._db = db;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task Inilialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (!await _roleManager.RoleExistsAsync(ConstDefs.ManagerRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(ConstDefs.ManagerRole));
                await _roleManager.CreateAsync(new IdentityRole(ConstDefs.KitchenRole));
                await _roleManager.CreateAsync(new IdentityRole(ConstDefs.CustomerRole));
                await _roleManager.CreateAsync(new IdentityRole(ConstDefs.FontDeskRole));

                var user = new ApplicationUser()
                {
                    UserName = "devtrainingapp@hotmail.com",
                    Email = "devtrainingapp@hotmail.com",
                    EmailConfirmed = true,
                    FirstName = "dev",
                    LastName = "trainingapp",
                };

                await _userManager.CreateAsync(user, "Admin123@");
                ApplicationUser userApp = await _db.ApplicationUser.FirstOrDefaultAsync(o => o.Email == "devtrainingapp@hotmail.com");
                await _userManager.AddToRoleAsync(userApp,ConstDefs.ManagerRole);   
            }
            return;
        }
    }
}
