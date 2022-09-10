using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Implementations;
using Restaurant.DAL.Interfaces;
using RestaurantUI.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Restaurant.Utility;
using Stripe;
using Restaurant.DAL.DbInitilizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add service DbContext
string strcon = builder.Configuration.GetConnectionString("RestaurantStrCon");
builder.Services.AddDbContext<RestaurantDBContext>(options => options.UseSqlServer(strcon));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<RestaurantDBContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

});

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

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
builder.Services.AddScoped<IDbInializer, DbInializer>();
//Add Sessions services
builder.Services.AddDistributedMemoryCache();   // stores items to memory 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;// cookie is acccessible by client side script
    options.Cookie.IsEssential = true;// if is essential for the applicaiotn to function corectly
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "387679563524550";
    options.AppSecret = "b6e059d7942aa27514438c409a115c12";

});


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
await SeedDatabase();
string key = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
StripeConfiguration.ApiKey = key;

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();

async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInializer>();
        await dbInitializer.Inilialize();
    }
}