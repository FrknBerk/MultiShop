using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService ContactService)
        {
            _contactService = ContactService;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactService.GetAllContactAsync();
            return Ok(values);
        }

        [HttpGet("GetFalseContactList")]
        public async Task<IActionResult> GetFalseContactList()
        {
            var values = await _contactService.GetFalseContactAsync();
            return Ok(values);
        }

        [HttpGet("GetFalseContactCount")]
        public async Task<IActionResult> GetFalseContactCount()
        {
            var values = await _contactService.GetFalseContactCount();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            var values = await _contactService.GetByIdContactAsync(id);
            return Ok(values);
        }
        
        [HttpGet("UpdateFalseContact")]
        public async Task<IActionResult> UpdateFalseContactAsync(string id)
        {
            await _contactService.UpdateFalseContactAsync(id);
            return Ok("Güncelleme işlemi başarılı");
        }


        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactService.CreateContactAsync(createContactDto);
            return Ok("İletişim Başarıyla Eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok("İletişim başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _contactService.UpdateContactAsync(updateContactDto);
            return Ok("İletişim başarıyla güncellendi");
        }
    }
}
