using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeadComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeadComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sekme başlığı ve meta açıklama Ayarlar ekranından gelir
            var setting = _context.Settings.FirstOrDefault() ?? new Setting { SiteTitle = "Portfolyo" };
            return View(setting);
        }
    }
}
