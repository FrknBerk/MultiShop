using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureDefaultComponentPartial : ViewComponent
    {
        //Öne Çıkanlar default
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
