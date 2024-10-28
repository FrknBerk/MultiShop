using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminContactViewComponents
{
    public class _AdminContactMessageComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IContactService _contactService;

        public _AdminContactMessageComponentPartial(IMessageService messageService, IContactService contactService)
        {
            _messageService = messageService;
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int contactMessageCount = await _contactService.GetFalseContactCountAsync();
            ViewBag.ContactMessageCount = contactMessageCount;
            var contact = await _contactService.GetFalseContactAsync();
            return View(contact);
        }
    }
}
