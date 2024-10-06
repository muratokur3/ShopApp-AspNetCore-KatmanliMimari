using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.WebUI.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Applicationcontext>(option=>option.UseSqlServer("Server=A00184508;Database=shopDb;User Id=sa;Password=12345678;Integrated Security=False;TrustServerCertificate=True;"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Applicationcontext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    //options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    

});

 builder.Services.ConfigureApplicationCookie(options =>
 {
    // Cookie settings

    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
     options.Cookie= new CookieBuilder
     {
         HttpOnly = true,
         Name = ".ShopApp.Security.Cookie",
         //SameSite = SameSiteMode.Strict
     };
     });




builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();


builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();


}
if (app.Environment.IsDevelopment())
{
    SeedDatabase.Seed();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseEndpoints(endpoints =>
{

    //Admin category create
    endpoints.MapControllerRoute(
        name: "admincategorycreate",
        pattern: "admin/categories/create",
        defaults: new { controller = "Admin", action = "CategoryCreate" });

 //Admin category list
    endpoints.MapControllerRoute(
        name: "admincategories",
        pattern: "admin/categories",
        defaults: new { controller = "Admin", action = "CategoryList" });

    //Admin category edit
    endpoints.MapControllerRoute(
        name: "admincategoryedit",
        pattern: "admin/categories/{id?}",
        defaults: new { controller = "Admin", action = "CategoryEdit" });

   
    //Admin product create
    endpoints.MapControllerRoute(
        name: "adminproductcreate",
        pattern: "admin/products/create",
        defaults: new { controller = "Admin", action = "ProductCreate" });

    //Admin product list
    endpoints.MapControllerRoute(
        name: "adminproductlist",
        pattern: "admin/products",
        defaults: new { controller = "Admin", action = "ProductList" });

    //Admin product edit
    endpoints.MapControllerRoute(
        name: "adminproductedit",
        pattern: "admin/products/{id?}",
        defaults: new { controller = "Admin", action = "ProductEdit" });


    //Product list
    endpoints.MapControllerRoute(
        name: "Products",
        pattern: "products/{category?}",
        defaults: new { controller = "shop", action = "List" });

    //Product search by name
    endpoints.MapControllerRoute(
        name: "search",
        pattern: "search",
        defaults: new { controller = "shop", action = "Search" });

    //Product details
    endpoints.MapControllerRoute(
        name: "productdetails",
        pattern: "{url}",
        defaults: new { controller = "shop", action = "Details" });

    //Home page
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
