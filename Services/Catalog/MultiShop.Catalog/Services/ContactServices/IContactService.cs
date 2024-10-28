using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Services.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<List<ResultContactDto>> GetFalseContactAsync();
        Task CreateContactAsync(CreateContactDto ContactDto);
        Task UpdateContactAsync(UpdateContactDto ContactDto);
        Task DeleteContactAsync(string id);
        Task<GetByIdContactDto> GetByIdContactAsync(string id);
        Task<int> GetFalseContactCount();
        Task UpdateFalseContactAsync(string id);
    }
}
