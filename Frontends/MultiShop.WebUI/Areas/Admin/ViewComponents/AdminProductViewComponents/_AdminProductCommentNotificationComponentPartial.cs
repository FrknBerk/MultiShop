using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminProductViewComponents
{
    public class _AdminProductCommentNotificationComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _AdminProductCommentNotificationComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.UnconfirmedProductComment = await _commentService.UnconfirmedProductCommentCountAsync();
            var values = await _commentService.UnconfirmedProductCommentAsync();
            return View(values);
        }
    }
}
