using MongoDB.Driver.Core.WireProtocol.Messages;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", categoryDto);            
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync("categories?id=" + id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return values;
        }

        public async Task<UpdateCategoryDto> GetByIdCategoryAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("categories/"+id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdateCategoryDto>();
            return values;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories",categoryDto);
        }
    }
}
