using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public DefaultController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        // İletişim formu: fetch ile JSON olarak gönderilir, sunucu tarafında doğrulanır
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(Message message)
        {
            // Bu alanları formdan kabul etme, sunucu belirler
            ModelState.Remove(nameof(Message.SendDate));
            ModelState.Remove(nameof(Message.IsRead));

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value!.Errors.First().ErrorMessage);
                return BadRequest(new { success = false, errors });
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // CV İndir: dosya Ayarlar ekranından yüklenir (wwwroot/uploads/cv), yolu Settings tablosunda tutulur
        public IActionResult DownloadCv()
        {
            var path = _context.Settings.Select(x => x.CvFilePath).FirstOrDefault();
            if (string.IsNullOrEmpty(path))
                return NotFound("CV henüz yüklenmemiş.");

            var uploadsRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath, "uploads"));
            var fullPath = Path.GetFullPath(Path.Combine(_env.WebRootPath, path.TrimStart('/')));
            if (!fullPath.StartsWith(uploadsRoot) || !System.IO.File.Exists(fullPath))
                return NotFound("CV dosyası bulunamadı.");

            var name = _context.Abouts.Select(x => x.NameSurname).FirstOrDefault() ?? "CV";
            var downloadName = $"{name.Replace(' ', '_')}_CV.pdf";
            return PhysicalFile(fullPath, "application/pdf", downloadName);
        }
    }
}
