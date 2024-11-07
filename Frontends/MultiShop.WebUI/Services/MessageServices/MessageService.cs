using MultiShop.DtoLayer.CommentDtos;
using MultiShop.DtoLayer.MessageDtos;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AdminAnswerUserMessageIdUpdateTrueAsync(int userMessageId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/AdminAnswerUserMessageIdUpdateTrue/" + userMessageId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<bool>(jsonData);
            return values;
        }

        public async Task<bool> CreateUserMessageAsync(CreateUserMessageDto createUserMessageDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync<CreateUserMessageDto>("usermessages", createUserMessageDto);
            if (responseMessage.IsSuccessStatusCode == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var result = await _httpClient.DeleteAsync("usermessages?id=" + id);
            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<ResultMessageDto>> GetAdminMessageListAsync(string adminId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetAdminMessageList/" + adminId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
            return values;
        }

        public async Task<int> GetAdminUnReadMessageTotalCountAsync(string adminId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetAdminUnReadMessageTotalCount/"+ adminId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<int>(jsonData);
            return values;
        }

        public async Task<List<ResultSendboxMessageDto>> GetByIdSendIdAsync(string sendId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetByIdSendId/" + sendId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSendboxMessageDto>>(jsonData);
            return values;
        }

        public async Task<int> GetFalseMessageCount()
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetFalseMessageCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<int>(jsonData);
            return values;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string recevierId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetMessageInBox?id=" + recevierId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultInboxMessageDto>>(jsonData);
            return values;
        }

        public Task<List<ResultInboxMessageDto>> GetMessageListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendMessageAsync(string sendId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetMessageSendBox?id=" + sendId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSendboxMessageDto>>(jsonData);
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetTotalMessageCountByReceiverId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<int>(jsonData);
            return values;
        }

        public async Task<List<ResultMessageDto>> GetUnReadMessageListAsync(string recevierId)
        {
            var responseMessage = await _httpClient.GetAsync("usermessages/GetUnReadMessageList/" + recevierId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
            return values;
        }
    }
}
