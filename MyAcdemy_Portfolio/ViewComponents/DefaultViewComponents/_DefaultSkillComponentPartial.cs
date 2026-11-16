using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sadece aktif yetenekler, grup adına göre gruplanmış (GroupBy)
            var groups = _context.Skills
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.Percentage)
                .ToList()
                .GroupBy(x => x.SkillCategory)
                .ToList();
            return View(groups);
        }
    }
}
