using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
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

        public ProductListController(IHttpClientFactory httpClientFactory, IUserService userService, ICommentService commentService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
            _commentService = commentService;
        }
        public IActionResult Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.i = id;
            return View();
        }
        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory3 = "Ürün Detayları";
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
    }
}
