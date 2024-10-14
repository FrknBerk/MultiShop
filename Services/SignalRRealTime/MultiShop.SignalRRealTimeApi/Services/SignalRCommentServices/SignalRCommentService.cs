using Newtonsoft.Json;

namespace MultiShop.SignalRRealTimeApi.Services.SignalRCommentServices
{
    public class SignalRCommentService : ISignalRCommentService
    {
        private readonly IHttpClientFactory _httpClient;

        public SignalRCommentService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> GetTotalCommentCount()
        {
            //var responseMessage = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<int>(jsonData);
            //return values;

            var client = _httpClient.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7075/api/CommentStatistics");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<int>(jsonData);
            return values;
        }
    }
}
