using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminMessageViewComponents
{
    public class _AdminUnReadMessageComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public _AdminUnReadMessageComponentPartial(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();
            ViewBag.AdminUnReadMessageCount = await _messageService.GetAdminUnReadMessageTotalCountAsync(user.Id);
            var values = await _messageService.GetUnReadMessageListAsync(user.Id);
            return View(values);
        }
    }
}
