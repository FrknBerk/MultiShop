﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IElasticSearchConnect _elasticSearchConnect;

        public ProductsController(IProductService productService, IElasticSearchConnect elasticSearchConnect)
        {
            _productService = productService;
            _elasticSearchConnect = elasticSearchConnect;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
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
            var defaultIndex = "products";
            var indexExists = _elasticSearchConnect.EsClient().Indices.Exists(defaultIndex);
            if (!indexExists.Exists)
            {
                var response = _elasticSearchConnect.EsClient().Indices.Create(defaultIndex,
                    index => index.Map<Product>(
                        x => x.AutoMap()));
            }
            var indexResponse = _elasticSearchConnect.EsClient().IndexDocument(createProductDto);
            await _productService.CreateProductAsync(createProductDto);
            return Ok("Ürün başarıyla Eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCAtegory(UpdateProductDto updateProductDto)
        {
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
    }
}
