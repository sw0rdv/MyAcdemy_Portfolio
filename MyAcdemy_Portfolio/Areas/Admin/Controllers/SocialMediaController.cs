using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialMediaController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("SocialMediaList");

        public IActionResult SocialMediaList()
        {
            var values = _context.SocialMedias
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.PlatformName)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            var nextOrder = (_context.SocialMedias.Max(x => (int?)x.DisplayOrder) ?? 0) + 1;
            return View(new SocialMedia { IsActive = true, DisplayOrder = nextOrder, IconClass = "bi-github" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid) return View(socialMedia);

            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya hesabı eklendi.";
            return RedirectToAction("SocialMediaList");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid) return View(socialMedia);

            _context.SocialMedias.Update(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya hesabı güncellendi.";
            return RedirectToAction("SocialMediaList");
        }

        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Sosyal medya hesabı silindi.";
            }
            return RedirectToAction("SocialMediaList");
        }

        public IActionResult ToggleStatus(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Hesap sitede gösteriliyor." : "Hesap sitede gizlendi.";
            }
            return RedirectToAction("SocialMediaList");
        }
    }
}
