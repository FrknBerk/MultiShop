using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("MyOrder")]
    public class MyOrderController : Controller
    {
        public IActionResult MyOrderList()
        {
            return View();
        }
    }
}
