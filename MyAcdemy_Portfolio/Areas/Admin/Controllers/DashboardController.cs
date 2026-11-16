using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAcdemy_Portfolio.Areas.Admin.Models;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                AdminName = _context.Abouts.Select(a => a.NameSurname).FirstOrDefault() ?? "Admin",

                TotalProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessageCount = _context.Messages.Count(),
                UnreadMessageCount = _context.Messages.Count(x => !x.IsRead),

                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),

                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.ProjectID)
                    .Take(3)
                    .ToList(),

                // Kategori bazında proje sayısı (projesi olmayan kategoriler de 0 ile görünsün diye kategoriden başlıyoruz)
                CategorySummary = _context.Categories
                    .GroupJoin(_context.Projects, c => c.CategoryID, p => p.CategoryID,
                        (c, projects) => new CategoryProjectCount
                        {
                            CategoryID = c.CategoryID,
                            CategoryName = c.CategoryName,
                            ProjectCount = projects.Count()
                        })
                    .OrderByDescending(x => x.ProjectCount)
                    .ToList()
            };

            return View(model);
        }
    }
}
