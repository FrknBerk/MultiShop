using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticService;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentStatisticService _commentStatisticService;
        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentStatisticService commentStatisticService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentStatisticService = commentStatisticService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) 
        
        {
            var user =await _userService.GetUserInfo();
            int messageCount = await _messageService.GetTotalMessageCountByReceiverId(user.Id);
            var getCommentCount = await _commentStatisticService.GetTotalCommentCount();
            ViewBag.NameSurname = user.Name + " " + user.SurName;
            ViewBag.Email = user.Email;
            ViewBag.Title = user.Name.Substring(0,1) + user.SurName.Substring(0,1);
            ViewBag.MessageCount = messageCount; 
            ViewBag.GetCommentCount = getCommentCount;
            return View(); 
        }
    }
}
