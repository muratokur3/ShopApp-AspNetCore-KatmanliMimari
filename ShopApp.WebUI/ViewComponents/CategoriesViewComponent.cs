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


        public IViewComponentResult Invoke()
        {


            if (RouteData.Values["action"].ToString() == "List")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_categoryServices.GetAll());
            return View();
        }

    }
}
