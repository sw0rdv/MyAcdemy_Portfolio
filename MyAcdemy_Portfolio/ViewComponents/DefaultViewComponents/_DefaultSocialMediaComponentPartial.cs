using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    /// <summary>
    /// Aktif sosyal medya hesaplarını gösterir. Üç yerde kullanılır:
    ///  - variant = "buttons" : ikon butonları (ana sayfa giriş bölümü ve footer)
    ///  - variant = "list"    : İletişim bölümündeki satırlar
    /// </summary>
    public class _DefaultSocialMediaComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSocialMediaComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string variant = "buttons")
        {
            var values = _context.SocialMedias
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.PlatformName)
                .ToList();

            return View(variant == "list" ? "List" : "Default", values);
        }
    }
}
