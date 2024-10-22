using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;


namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {

        private ICategoryService _categoryServices;
        public CategoriesViewComponent(ICategoryService categoryServices)
        {
            this._categoryServices = categoryServices;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            if (RouteData.Values["category"] !=null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(await _categoryServices.GetAll());
        }

    }
}
