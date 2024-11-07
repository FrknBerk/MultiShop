using MultiShop.DtoLayer.MessageDtos;
using System.Drawing;

namespace MultiShop.WebUI.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string recevierId);
        Task<List<ResultSendboxMessageDto>> GetSendMessageAsync(string sendId);
        Task<List<ResultInboxMessageDto>> GetMessageListAsync();
        Task<int> GetTotalMessageCountByReceiverId(string id);
        Task<int> GetFalseMessageCount();
        //Task CreateMessageCouponAsync(CreateMessageDto createMessageDto);
        //Task UpdateMessageCouponAsync(UpdateMessageDto updateMessageDto);
        Task<bool> DeleteMessageAsync(int id);
        //Task<GetByIdMessageDto> GetByIdMessageCouponAsync(int id);
        Task<List<ResultSendboxMessageDto>> GetByIdSendIdAsync(string sendId);
        Task<bool> CreateUserMessageAsync(CreateUserMessageDto createUserMessageDto);
        Task<List<ResultMessageDto>> GetAdminMessageListAsync(string adminId);
        Task<int> GetAdminUnReadMessageTotalCountAsync(string adminId);
        Task<List<ResultMessageDto>> GetUnReadMessageListAsync(string recevierId);
        Task<bool> AdminAnswerUserMessageIdUpdateTrueAsync(int userMessageId);
    }
}
