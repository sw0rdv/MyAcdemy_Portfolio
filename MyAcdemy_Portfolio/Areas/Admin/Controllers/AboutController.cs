using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        
        private readonly PortfolioContext DbContext;

        public AboutController(PortfolioContext dbContext)
        {
            DbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var values = DbContext.Abouts.FirstOrDefault();

            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About abouts)
        {
            DbContext.Abouts.Update(abouts);
            DbContext.SaveChanges();
            return RedirectToAction("UpdateAbout");
        }
    }
}
