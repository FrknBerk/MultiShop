using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public class AboutService :IAboutService
    {
        private readonly HttpClient _httpClient;

        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAboutAsync(CreateAboutDto AboutDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAboutDto>("abouts", AboutDto);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync("abouts?id=" + id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var responseMessage = await _httpClient.GetAsync("abouts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            return values;
        }

        public async Task<UpdateAboutDto> GetByIdAboutAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("abouts/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
            return values;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto AboutDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateAboutDto>("abouts", AboutDto);
        }
    }
}
