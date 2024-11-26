using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ElasticSearchServices;
using MultiShop.Catalog.Services.ProductServices;
using System.ComponentModel;

namespace MultiShop.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IElasticSearchService _elasticSearchService;

        public ProductsController(IProductService productService, IElasticSearchService elasticSearchService)
        {
            _productService = productService;
            _elasticSearchService = elasticSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {

            var result1 = await _elasticSearchService.FuzzyQueryAsync<Product>(x => x.ProductName, "gömlek", ElasticsearchIndexes.Product);
            var resul2 = await _elasticSearchService.BoolQueryAsync<Product>(x => x.ProductName, "keten", x => x.ProductName, "göm", x => x.ProductName, "*g*",ElasticsearchIndexes.Product);
            var result = await _elasticSearchService.GetDocumentsAsync<Product>(ElasticsearchIndexes.Product);
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateProductDto createProductDto)
        {
            _elasticSearchService.CreateDocumentAsync(createProductDto);
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Ürün başarıyla Eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var product = await _productService.GetByIdProductAsync(id);
            _elasticSearchService.DeleteDocumentAsync<Product>(product.Id.ToString(),ElasticsearchIndexes.Product);
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCAtegory(UpdateProductDto updateProductDto)
        {
            await _elasticSearchService.UpdateDocumentAsync<Product>(updateProductDto.Id.ToString(), updateProductDto, ElasticsearchIndexes.Product);
            await _productService.UpdateProductAsync(updateProductDto);
            return Ok("Ürün başarıyla güncellendi");
        }

        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductcWithCategoryAsync();
            return Ok(values);
        }

        [HttpGet("ProductListWithCategoryByCategoryId")]
        public async Task<IActionResult> ProductListWithCategory(string categoryId)
        {
            var values = await _productService.GetProductsWithCategoryByCategoryIdAsync(categoryId);
            return Ok(values);
        }

        [HttpGet("SearchProductName")]
        public async Task<IActionResult> SearchProductNameAsync(string productName)
        {
            var values = await _elasticSearchService.WildcardQueryAsync<Product>(p => p.ProductName,productName, ElasticsearchIndexes.Product);
            return Ok(values);
        }
    }
}
