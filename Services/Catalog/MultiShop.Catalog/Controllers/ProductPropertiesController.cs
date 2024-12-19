using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductPropertyDtos;
using MultiShop.Catalog.Services.ProductPropertyServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPropertiesController : ControllerBase
    {
        private readonly IProductPropertyService _productPropertyService;

        public ProductPropertiesController(IProductPropertyService productPropertyService)
        {
            _productPropertyService = productPropertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductPropertyAsync()
        {
            var values = await _productPropertyService.GetAllProductPropertyAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByPropertyIdProductPropertyAsync(string id)
        {
            var value = await _productPropertyService.GetByIdProductPropertyAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductPropertyAsync(CreateProductPropertyDto createProductPropertyDto)
        {
            await _productPropertyService.CreateProductPropertyAsync(createProductPropertyDto);
            return Ok("Ürün özelliği başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductPropertyAsync(string id)
        {
            await _productPropertyService.DeleteProductPropertyAsync(id);
            return Ok("Ürün özelliği başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductPropertyAsync(UpdateProductPropertyDto updateProductPropertyDto)
        {
            await _productPropertyService.UpdateProductPropertyAsync(updateProductPropertyDto);
            return Ok("Ürün başarıyla güncellendi");
        }
    }
}
