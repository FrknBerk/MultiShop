using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FavoriteProductDtos;
using MultiShop.Catalog.Services.FavoriteProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteProductsController : ControllerBase
    {
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoriteProductsController(IFavoriteProductService favoriteProductService)
        {
            _favoriteProductService = favoriteProductService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteProductAsync(CreateFavoriteProductDto createFavoriteProductDto)
        {
            await _favoriteProductService.CreateFavoriteProductAsync(createFavoriteProductDto);
            return Ok("Favori ürün başarıyla eklendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserIdFavoriteProcutListAsync(string id)
        {
            var result = await _favoriteProductService.GetByUserIdListAsync(id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteProductAsync(string id)
        {
            await _favoriteProductService.DeleteFavoriteProductAsync(id);
            return Ok("Favori ürün başarıyla silindi");
        }
    }
}
