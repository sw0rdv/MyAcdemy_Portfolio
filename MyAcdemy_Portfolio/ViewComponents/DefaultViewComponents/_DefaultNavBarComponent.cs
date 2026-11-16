using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavBarComponent : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultNavBarComponent(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // CV yüklenmişse "CV İndir" butonu gösterilir
            ViewBag.HasCv = _context.Settings.Any(x => x.CvFilePath != null);
            ViewBag.NameSurname = _context.HeroSections.Select(x => x.Title).FirstOrDefault()
                                  ?? _context.Abouts.Select(x => x.NameSurname).FirstOrDefault()
                                  ?? "Portfolyo";
            return View();
        }
    }
}
