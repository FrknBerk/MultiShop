using MultiShop.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.PropertyTypeServices
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly HttpClient _httpClient;

        public PropertyTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto)
        {
            await _httpClient.PostAsJsonAsync<CreatePropertyTypeDto>("propertytypes", createPropertyTypeDto);
        }

        public async Task DeletePropertyTypeAsync(string id)
        {
            await _httpClient.DeleteAsync("propertytypes?id=" + id);
        }

        public async Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync()
        {
            var responseMessage = await _httpClient.GetAsync("propertytypes");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultPropertyTypeDto>>(jsonData);
            return values;
        }

        public async Task<UpdatePropertyTypeDto> GetByIdPropertyTypeAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("propertytypes/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdatePropertyTypeDto>();
            return values;
        }

        public async Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            await _httpClient.PutAsJsonAsync<UpdatePropertyTypeDto>("propertytypes", updatePropertyTypeDto);
        }
    }
}
