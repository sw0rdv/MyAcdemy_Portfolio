using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;
using MyAcdemy_Portfolio.Helpers;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index() => RedirectToAction("UpdateSetting");

        [HttpGet]
        public IActionResult UpdateSetting()
        {
            // Tek kayıt mantığı
            var value = _context.Settings.FirstOrDefault() ?? new Setting();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSetting(Setting setting, IFormFile? cvFile)
        {
            if (cvFile != null)
            {
                var error = FileUploadHelper.ValidatePdf(cvFile);
                if (error != null) ModelState.AddModelError("cvFile", error);
            }

            if (!ModelState.IsValid) return View(setting);

            if (cvFile != null)
            {
                // Yeni CV'yi kaydet, eskisini sil
                var oldPath = setting.CvFilePath;
                setting.CvFilePath = await FileUploadHelper.SaveAsync(cvFile, _env, "cv", "cv");
                setting.CvUpdatedDate = DateTime.Now;
                FileUploadHelper.DeleteIfLocal(oldPath, _env);
            }

            if (setting.SettingID == 0) _context.Settings.Add(setting);
            else _context.Settings.Update(setting);

            _context.SaveChanges();
            TempData["Success"] = cvFile != null ? "Ayarlar ve CV güncellendi." : "Ayarlar güncellendi.";
            return RedirectToAction("UpdateSetting");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCv()
        {
            var setting = _context.Settings.FirstOrDefault();
            if (setting?.CvFilePath != null)
            {
                FileUploadHelper.DeleteIfLocal(setting.CvFilePath, _env);
                setting.CvFilePath = null;
                setting.CvUpdatedDate = null;
                _context.SaveChanges();
                TempData["Success"] = "CV kaldırıldı.";
            }
            return RedirectToAction("UpdateSetting");
        }
    }
}
