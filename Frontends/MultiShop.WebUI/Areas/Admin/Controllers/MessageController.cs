using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.MailDtos;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Services.IdentityServices.UserIdentityServices;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MailServices;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IMessageService _messageService;
        private readonly IMailService _mailService;
        public MessageController(IUserService userService, IMessageService messageService, IUserIdentityService userIdentityService, IMailService mailService)
        {
            _userService = userService;
            _messageService = messageService;
            _userIdentityService = userIdentityService;
            _mailService = mailService;
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
            await _messageService.AdminAnswerUserMessageIdUpdateTrueAsync(Convert.ToInt32(userMessageId));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AnswerMessage(CreateUserMessageDto createUserMessageDto)
        {
            var user = await _userService.GetUserInfo();
            var answerUser = await _userIdentityService.GetByIdUser(createUserMessageDto.ReceiverId);
            createUserMessageDto.SendedId = user.Id;
            createUserMessageDto.IsRead = true;
            createUserMessageDto.MessageDate = DateTime.UtcNow;
            createUserMessageDto.AnsweredIf = createUserMessageDto.ReceiverId;
            var result = await _messageService.CreateUserMessageAsync(createUserMessageDto);
            if (result == true)
            {
                var createSendMail = new CreateMailSendDto
                {
                    Body = "<div style='padding:30px;margin:10px;'><h1>Sayın " + answerUser.Name + " " + answerUser.Surname + " </h1><br/><p>Mesajınıza geri dönüş yapılmıştır Lütfen mesajlarınızı kontrol ediniz</p><br/></div>",
                    Subject = createUserMessageDto.Subject,
                    To = answerUser.Email,
                };
                _mailService.SendMail(createSendMail);
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
