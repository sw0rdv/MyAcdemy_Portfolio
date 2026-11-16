using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("CategoryList");

        public IActionResult CategoryList()
        {
            // Include ile her kategorinin projelerini de getir (proje sayısı için)
            var values = _context.Categories
                .Include(x => x.Projects)
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (_context.Categories.Any(x => x.CategoryName == category.CategoryName))
                ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");

            if (!ModelState.IsValid) return View(category);

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category category)
        {
            if (_context.Categories.Any(x => x.CategoryName == category.CategoryName && x.CategoryID != category.CategoryID))
                ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");

            if (!ModelState.IsValid) return View(category);

            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Include(x => x.Projects).FirstOrDefault(x => x.CategoryID == id);
            if (value == null) return RedirectToAction("CategoryList");

            // Projesi olan kategori silinirse projeler de (cascade) silinir; bunu engelle
            if (value.Projects != null && value.Projects.Any())
            {
                TempData["Error"] = $"\"{value.CategoryName}\" kategorisinde {value.Projects.Count} proje var. Önce projeleri başka kategoriye taşıyın.";
                return RedirectToAction("CategoryList");
            }

            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction("CategoryList");
        }
    }
}
