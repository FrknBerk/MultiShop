using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string recevierId);
        Task<List<ResultSendboxMessageDto>> GetSendMessageAsync(string sendId);
        Task<int> GetTotalMessageCountByReceiverId(string id);
        //Task CreateMessageCouponAsync(CreateMessageDto createMessageDto);
        //Task UpdateMessageCouponAsync(UpdateMessageDto updateMessageDto);
        //Task DeleteMessageCouponAsync(int id);
        //Task<GetByIdMessageDto> GetByIdMessageCouponAsync(int id);
    }
}
