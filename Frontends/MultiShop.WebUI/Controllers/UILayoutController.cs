using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.TokenService;
using IdentityModel.AspNetCore.AccessTokenManagement;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult _UILayout()
        {
            return View();
        }
    }
}
