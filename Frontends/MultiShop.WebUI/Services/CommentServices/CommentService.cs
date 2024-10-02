using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCommentDto>> CommentListByProductId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("comments/CommentListByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return values;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCommentDto>("comments", createCommentDto);
        }

        public async Task DeleteCommentAsync(string id)
        {
            await _httpClient.DeleteAsync("comments?id=" + id);
        }

        public async Task<List<UpdateCommentDto>> GetAllCommentAsync()
        {
            var responseMessage = await _httpClient.GetAsync("comments");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<UpdateCommentDto>>(jsonData);
            return values;
        }

        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
        {
            var reponseMessage = await _httpClient.GetAsync("comments/" + id);
            var values = await reponseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>();
            return values;
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCommentDto>("comments", updateCommentDto);
        }

    }
}
