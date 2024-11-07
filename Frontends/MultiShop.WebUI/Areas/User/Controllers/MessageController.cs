using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Services.IdentityServices.RoleIdentityServices;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class MessageController : Controller
    { 
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IRoleIdentityService _roleIdentityService;

        public MessageController(IMessageService messageService, IUserService userService, IRoleIdentityService roleIdentityService)
        {
            _messageService = messageService;
            _userService = userService;
            _roleIdentityService = roleIdentityService;
        }

        public async Task<IActionResult> Inbox()
        {
            var user = await _userService.GetUserInfo();
            var values = await _messageService.GetInboxMessageAsync(user.Id);
            return View(values);
        }

        public async Task<IActionResult> Sendbox()
        {
            var user = await _userService.GetUserInfo();
            var values = await _messageService.GetSendMessageAsync(user.Id);
            return View(values);
        }

        public async Task<IActionResult> CreateUserMessage()
        {
            var user = await _userService.GetUserInfo();
            var adminUser = await _roleIdentityService.GetUsersInRoleAsync("Admin");
            ViewBag.AdminUserId = adminUser.FirstOrDefault().Id;
            ViewBag.UserId = user.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserMessage(CreateUserMessageDto createUserMessageDto)
        {
            var user = await _userService.GetUserInfo();
            var admin = await _roleIdentityService.GetUsersInRoleAsync("Admin");
            createUserMessageDto.ReceiverId = admin.FirstOrDefault().Id;
            createUserMessageDto.SendedId = user.Id;
            createUserMessageDto.IsRead = false;
            createUserMessageDto.MessageDate = DateTime.UtcNow;
            createUserMessageDto.AnsweredIf = user.Id;
            var result = await _messageService.CreateUserMessageAsync(createUserMessageDto);
            if (result == true)
            {
                return RedirectToAction("CreateUserMessage", "Message", new { area = "User" });
            }
            return View();
        }

        public async Task<JsonResult> GetUserMessageList()
        {
            var user = await _userService.GetUserInfo();
            var adminUser = await _roleIdentityService.GetUsersInRoleAsync("Admin");
            ViewBag.AdminUserId = adminUser.FirstOrDefault().Id;
            ViewBag.UserId = user.Id;
            var values = await _messageService.GetByIdSendIdAsync(user.Id);
            return Json(values);
        }
    }
}
