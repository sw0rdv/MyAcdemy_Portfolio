using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("ExperienceList");

        public IActionResult ExperienceList()
        {
            // Tarih sırası: en yeni başlangıç yılı en üstte
            var values = _context.Experiences
                .OrderByDescending(x => x.StartYear)
                .ThenByDescending(x => x.ExperienceID)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience { IsActive = true, StartYear = DateTime.Now.Year });
        }

        // Eski (yazım hatalı) adres bozulmasın
        public IActionResult CreateExprience() => RedirectToAction("CreateExperience");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid) return View(experience);

            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim eklendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid) return View(experience);

            _context.Experiences.Update(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim güncellendi.";
            return RedirectToAction("ExperienceList");
        }

        public IActionResult DeleteExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value != null)
            {
                _context.Experiences.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Deneyim silindi.";
            }
            return RedirectToAction("ExperienceList");
        }

        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Deneyim aktif edildi." : "Deneyim pasife alındı.";
            }
            return RedirectToAction("ExperienceList");
        }
    }
}
