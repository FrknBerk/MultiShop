using MultiShop.DtoLayer.CommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<UpdateCommentDto>> GetAllCommentAsync();
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task DeleteCommentAsync(string id);
        Task<UpdateCommentDto> GetByIdCommentAsync(string id);
        Task<List<ResultCommentDto>> CommentListByProductId(string id);
    }
}
