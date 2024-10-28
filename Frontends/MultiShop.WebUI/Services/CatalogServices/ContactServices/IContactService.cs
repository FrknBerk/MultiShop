using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<List<ResultContactDto>> GetFalseContactAsync();
        Task CreateContactAsync(CreateContactDto ContactDto);
        Task UpdateContactAsync(UpdateContactDto ContactDto);
        Task UpdateFalseContactAsync(string id);
        Task DeleteContactAsync(string id);
        Task<GetByIdContactDto> GetByIdContactAsync(string id);
        Task<int> GetFalseContactCountAsync();
    }
}
