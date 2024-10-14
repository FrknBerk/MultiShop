namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticService
{
    public interface IDiscountStatisticService
    {
        Task<int> GetDiscountCouponCount();
    }
}
