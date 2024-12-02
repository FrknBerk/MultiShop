using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IProductService _productService;

        public ProductListController(IHttpClientFactory httpClientFactory, IUserService userService, ICommentService commentService, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
            _commentService = commentService;
            _productService = productService;
        }
        public IActionResult Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.i = id;
            return View();
        }
        public async Task<IActionResult> ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory3 = "Ürün Detayları";
            var product = await _productService.GetByIdProductAsync(id);
            if (product != null)
                ViewBag.x = product.ProductId;
            else
                ViewBag.x = id;
            return View();
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<PartialViewResult> AddComment(CreateCommentDto createCommentDto)
        {
            var user = await _userService.GetUserInfo();
            if (createCommentDto.Rating <= 0)
                createCommentDto.Rating = 0;
            createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToString());
            createCommentDto.Status = false;
            createCommentDto.UserId = user.Id;
            createCommentDto.NameSurname = user.Name + " " + user.SurName;
            createCommentDto.Email = user.Email;
            await _commentService.CreateCommentAsync(createCommentDto);            
            return PartialView("AddComment");
        }

        public async Task<JsonResult> SearchProductName(string productName)
        {
            var values = await _productService.SearchProductNameAsync(productName);
            return Json(values);
        }
    }
}
