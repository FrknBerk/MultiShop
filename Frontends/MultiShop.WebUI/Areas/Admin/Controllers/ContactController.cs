using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [Route("ContactList")]
        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactService.GetAllContactAsync();
            return View(values);
        }
        [Route("ContactDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ContactDetail(string id)
        {
            await _contactService.UpdateFalseContactAsync(id);
            var value = await _contactService.GetByIdContactAsync(id);
            return View(value);
        }

        [Route("DeleteContact/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction("ContactList", "Contact");
        }
    }
}
