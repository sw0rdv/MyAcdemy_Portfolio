using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroSectionController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UpdateHeroSection()
        {
            // Tek kayıt mantığı: varsa ilk kaydı getir, yoksa boş form aç
            var value = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateHeroSection(HeroSection heroSection)
        {
            if (!ModelState.IsValid)
            {
                return View(heroSection);
            }

            if (heroSection.HeroSectionId == 0)
                _context.HeroSections.Add(heroSection);
            else
                _context.HeroSections.Update(heroSection);

            _context.SaveChanges();
            TempData["Success"] = "Ana sayfa giriş bölümü güncellendi.";
            return RedirectToAction("UpdateHeroSection");
        }
    }
}
