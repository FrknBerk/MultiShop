using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.PropertyTypeDtos;
using MultiShop.Catalog.Services.PropertyTypeServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPropertyTypeAsync()
        {
            var values = await _propertyTypeService.GetAllPropertyTypeAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdPropertyTypeAsync(string id)
        {
            var values = await _propertyTypeService.GetByIdPropertyTypeAsync(id);
            return Ok(values);
        }

        [HttpGet("GetByCategoryIdPropertyType/{id}")]
        public async Task<IActionResult> GetByCategoryIdPropertyTypeAsync(string id)
        {
            var values = await _propertyTypeService.GetByCategoryIdPropertyTypeAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertTypeAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            await _propertyTypeService.CreatePropertyTypeAsync(createPropertyTypeDto);
            return Ok("Özellik başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePropertTypeAsync(string id)
        {
            await _propertyTypeService.DeletePropertyTypeAsync(id);
            return Ok("Özellik başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            await _propertyTypeService.UpdatePropertyTypeAsync(updatePropertyTypeDto);
            return Ok("Özellik başarıyla güncellendi");
        }
    }
}
