using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("EducationList");

        public IActionResult EducationList()
        {
            // Tarih sırası: en yeni başlangıç yılı en üstte
            var values = _context.Educations
                .OrderByDescending(x => x.StartYear)
                .ThenByDescending(x => x.EducationID)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View(new Education { IsActive = true, StartYear = DateTime.Now.Year });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEducation(Education education)
        {
            if (!ModelState.IsValid) return View(education);

            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi eklendi.";
            return RedirectToAction("EducationList");
        }

        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEducation(Education education)
        {
            if (!ModelState.IsValid) return View(education);

            _context.Educations.Update(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi güncellendi.";
            return RedirectToAction("EducationList");
        }

        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value != null)
            {
                _context.Educations.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Eğitim bilgisi silindi.";
            }
            return RedirectToAction("EducationList");
        }

        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Educations.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Eğitim aktif edildi." : "Eğitim pasife alındı.";
            }
            return RedirectToAction("EducationList");
        }
    }
}
