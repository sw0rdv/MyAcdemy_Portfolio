using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;
using MyAcdemy_Portfolio.Helpers;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index() => RedirectToAction("ProjectList");

        public IActionResult ProjectList()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .OrderByDescending(x => x.ProjectID)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            ViewBag.Categories = GetCategories();
            return View(new Project { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                var error = FileUploadHelper.ValidateImage(imageFile);
                if (error != null) ModelState.AddModelError("imageFile", error);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategories();
                return View(project);
            }

            if (imageFile != null)
                project.ProjectImageUrl = await FileUploadHelper.SaveAsync(imageFile, _env, "projects", "project");

            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje eklendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            ViewBag.Categories = GetCategories();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                var error = FileUploadHelper.ValidateImage(imageFile);
                if (error != null) ModelState.AddModelError("imageFile", error);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategories();
                return View(project);
            }

            var oldImage = _context.Projects.AsNoTracking()
                .Where(x => x.ProjectID == project.ProjectID)
                .Select(x => x.ProjectImageUrl)
                .FirstOrDefault();

            if (imageFile != null)
            {
                project.ProjectImageUrl = await FileUploadHelper.SaveAsync(imageFile, _env, "projects", "project");
            }

            // Görsel değiştiyse eski yüklenmiş dosyayı sil
            if (oldImage != project.ProjectImageUrl)
                FileUploadHelper.DeleteIfLocal(oldImage, _env);

            _context.Projects.Update(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje güncellendi.";
            return RedirectToAction("ProjectList");
        }

        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                FileUploadHelper.DeleteIfLocal(value.ProjectImageUrl, _env);
                _context.Projects.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Proje silindi.";
            }
            return RedirectToAction("ProjectList");
        }

        // Aktif/Pasif durumunu tek tıkla değiştir
        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Proje yayına alındı." : "Proje pasife alındı.";
            }
            return RedirectToAction("ProjectList");
        }

        private List<SelectListItem> GetCategories()
        {
            return _context.Categories
                .OrderBy(x => x.CategoryName)
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList();
        }
    }
}
