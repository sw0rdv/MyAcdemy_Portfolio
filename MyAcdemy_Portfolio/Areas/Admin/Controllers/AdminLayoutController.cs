using Microsoft.AspNetCore.Mvc;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
        // AdminLayout/Index.cshtml bir layout dosyasıdır, tek başına sayfa olarak açılamaz
        // (RenderSectionAsync hatası verir). Bu adrese gelen olursa Dashboard'a yönlendir.
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
    }
}
