using Microsoft.AspNetCore.Mvc;


namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //if(RouteData.Values["action"].ToString() == "List")
            //     ViewBag.SelectedCategory = RouteData?.Values["id"];
            //return View(CategoryRepository.Categories);
            return View();
        }

    }
}
