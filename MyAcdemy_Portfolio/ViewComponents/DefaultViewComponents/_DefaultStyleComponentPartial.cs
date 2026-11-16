using Microsoft.AspNetCore.Mvc;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
