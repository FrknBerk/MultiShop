using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactDto ContactDto)
        {
            await _httpClient.PostAsJsonAsync<CreateContactDto>("contacts", ContactDto);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync("contacts?id=" + id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var responseMessage = await _httpClient.GetAsync("contacts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
            return values;
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("contacts/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<GetByIdContactDto>();
            return values;
        }

        public async Task UpdateContactAsync(UpdateContactDto ContactDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateContactDto>("contacts", ContactDto);
        }
    }
}
