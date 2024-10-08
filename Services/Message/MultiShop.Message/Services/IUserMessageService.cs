using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services
{
    public interface IUserMessageService
    {
        Task<List<ResultMessageDto>> GetAllMessageAsync();
        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string recevierId);
        Task<List<ResultSendboxMessageDto>> GetSendMessageAsync(string sendId);
        Task CreateMessageCouponAsync(CreateMessageDto createMessageDto);
        Task UpdateMessageCouponAsync(UpdateMessageDto updateMessageDto);
        Task DeleteMessageCouponAsync(int id);
        Task<GetByIdMessageDto> GetByIdMessageCouponAsync(int id);
    }
}
