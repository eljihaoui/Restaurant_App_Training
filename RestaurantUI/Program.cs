using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Implementations;
using Restaurant.DAL.Interfaces;
using RestaurantUI.Profiles;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add service DbContext
string strcon = builder.Configuration.GetConnectionString("RestaurantStrCon");
builder.Services.AddDbContext<RestaurantDBContext>(options => options.UseSqlServer(strcon));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<RestaurantDBContext>();

// Add service NToastNotify
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopCenter,
    PreventDuplicates = true,
    CloseButton = true
});

// Add Service UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
