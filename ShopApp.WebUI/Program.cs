using ShopApp.Business.Abstratc;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
