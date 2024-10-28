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
    }
}
