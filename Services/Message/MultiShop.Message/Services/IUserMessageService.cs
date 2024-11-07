using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        Task<bool> DeleteMessageAsync(int id);
        Task<GetByIdMessageDto> GetByIdMessageCouponAsync(int id);
        Task<int> GetTotalMessageCountAsync();
        Task<int> GetFalseMessageCountAsync();

        Task<int> GetTotalMessageCountByReceiverId(string id);
        Task<List<ResultSendboxMessageDto>> GetByIdSendIdAsync(string sendId);
        Task<List<ResultMessageDto>> GetAdminMessageListAsync(string adminId);
        Task<int> GetAdminUnReadMessageTotalCountAsync(string  adminId);
        Task<List<ResultMessageDto>> GetUnReadMessageList(string receiverId);
        Task<bool> AdminAnswerUserMessageIdUpdateTrue(int id);
    }
}
