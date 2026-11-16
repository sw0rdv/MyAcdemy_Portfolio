using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("SkillList");

        public IActionResult SkillList()
        {
            var values = _context.Skills
                .OrderBy(x => x.SkillCategory)
                .ThenByDescending(x => x.Percentage)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            ViewBag.Groups = GetGroups();
            return View(new Skill { IsActive = true, Percentage = 50 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = GetGroups();
                return View(skill);
            }

            _context.Skills.Add(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek eklendi.";
            return RedirectToAction("SkillList");
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();

            ViewBag.Groups = GetGroups();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = GetGroups();
                return View(skill);
            }

            _context.Skills.Update(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek güncellendi.";
            return RedirectToAction("SkillList");
        }

        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value != null)
            {
                _context.Skills.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek silindi.";
            }
            return RedirectToAction("SkillList");
        }

        // Formda öneri olarak gösterilecek mevcut grup adları
        private List<string> GetGroups()
        {
            return _context.Skills.Select(x => x.SkillCategory).Distinct().OrderBy(x => x).ToList();
        }
    }
}
