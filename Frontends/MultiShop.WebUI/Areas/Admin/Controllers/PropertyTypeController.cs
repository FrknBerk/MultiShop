using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.PropertyTypeDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.PropertyTypeServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/PropertyType")]
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ICategoryService _categoryService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService, ICategoryService categoryService)
        {
            _propertyTypeService = propertyTypeService;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _propertyTypeService.GetAllPropertyTypeAsync();
            return View(values);
        }

        [Route("CreatePropertyType")]
        public async Task<IActionResult> CreatePropertyType()
        {
            var category = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> dropdownCategory = (from x in category
                                    select new SelectListItem
                                    {
                                        Text = x.CategoryName,
                                        Value = x.CategoryId
                                    }).ToList();
            ViewBag.Category = dropdownCategory;
            return View();
        }

        [Route("CreatePropertyType")]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType(CreatePropertyTypeDto createPropertyTypeDto)
        {
            await _propertyTypeService.CreatePropertyTypeAsync(createPropertyTypeDto);
            return View("Index");
        }

        [Route("DeletePropertyType/{id}")]
        public async Task<IActionResult> DeletePropertyType(string id)
        {
            await _propertyTypeService.DeletePropertyTypeAsync(id);
            return View("Index");
        }

        [Route("UpdatePropertyType/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdatePropertyType(string id)
        {
            var category = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> dropdownCategory = (from x in category
                                                     select new SelectListItem
                                                     {
                                                         Text = x.CategoryName,
                                                         Value = x.CategoryId
                                                     }).ToList();
            ViewBag.Category = dropdownCategory;
            var values = await _propertyTypeService.GetByIdPropertyTypeAsync(id);
            return View(values);
        }

        [Route("UpdatePropertyType/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyType(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            await _propertyTypeService.UpdatePropertyTypeAsync(updatePropertyTypeDto);
            return RedirectToAction("Index", "PropertyType", new { area = "Admin" });
        }
    }
}
