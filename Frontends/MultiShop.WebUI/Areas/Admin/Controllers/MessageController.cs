using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        public MessageController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public async Task<IActionResult> MessageList()
        {
            var admin = await _userService.GetUserInfo();
            var values = await _messageService.GetAdminMessageListAsync(admin.Id);
            return View(values);
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var result = await _messageService.DeleteMessageAsync(id);
            if (result)
                return RedirectToAction("MessageList", "Message", new { area = "Admin" });
            return View();
        }

        public async Task<IActionResult> AnswerMessage(string id,string userMessageId)
        {
            var user = await _userService.GetUserInfo();
            ViewBag.SendId = id.Remove(0,3);
            ViewBag.UserMessageId = userMessageId;
            ViewBag.AdminUserId = user.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnswerMessage(CreateUserMessageDto createUserMessageDto)
        {
            var user = await _userService.GetUserInfo();
            createUserMessageDto.SendedId = user.Id;
            createUserMessageDto.IsRead = true;
            createUserMessageDto.MessageDate = DateTime.UtcNow;
            createUserMessageDto.AnsweredIf = createUserMessageDto.ReceiverId;
            var result = await _messageService.CreateUserMessageAsync(createUserMessageDto);
            if (result == true)
            {
                var update = await _messageService.AdminAnswerUserMessageIdUpdateTrueAsync(createUserMessageDto.UserMessageId);
                if(update == true)
                    return RedirectToAction("MessageList", "Message", new { area = "Admin" });
            }
            return View();
        }

        public async Task<JsonResult>  GetAdminMessageList(string id)
        {
            var values = await _messageService.GetByIdSendIdAsync(id);
            return Json(values);
        }
    }
}
