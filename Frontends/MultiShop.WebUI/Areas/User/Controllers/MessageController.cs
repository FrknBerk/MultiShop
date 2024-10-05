using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    public class MessageController : Controller
    {
        [Area("Message")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
