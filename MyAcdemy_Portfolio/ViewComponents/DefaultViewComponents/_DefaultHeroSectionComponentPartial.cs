using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Kayıt yoksa sayfa hata vermesin diye boş nesne
            var value = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            ViewBag.Contact = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(value);
        }
    }
}
