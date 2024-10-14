namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticService
{
    public interface ICommentStatisticService
    {
        Task<int> GetTotalCommentCount();
        Task<int> GetActiveCommentCount();
        Task<int> GetPassiveCommentCount();
    }
}
