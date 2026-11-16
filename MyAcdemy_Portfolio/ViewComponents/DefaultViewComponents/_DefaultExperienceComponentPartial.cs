using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sadece aktif kayıtlar, tarih sırasına göre (en yeni üstte)
            var values = _context.Experiences
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.StartYear)
                .ThenByDescending(x => x.ExperienceID)
                .ToList();
            return View(values);
        }
    }
}
