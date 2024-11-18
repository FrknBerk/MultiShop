using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ToastrViewComponents
{
	public class _ToastrComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
