using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("MessageList");

        // Arama (gönderen / e-posta / konu), durum filtresi ve tarih sıralaması sunucu tarafında
        public IActionResult MessageList(string? search, string? status, string sort = "desc")
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(x => x.NameSurname.Contains(s) || x.Email.Contains(s) || x.Subject.Contains(s));
            }

            if (status == "unread") query = query.Where(x => !x.IsRead);
            else if (status == "read") query = query.Where(x => x.IsRead);

            query = sort == "asc"
                ? query.OrderBy(x => x.SendDate)
                : query.OrderByDescending(x => x.SendDate);

            ViewBag.Search = search;
            ViewBag.Status = status ?? "all";
            ViewBag.Sort = sort;
            ViewBag.TotalCount = _context.Messages.Count();
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);

            return View(query.ToList());
        }

        // Dashboard'daki eski linkler için: listeyi açar ve ilgili mesajın modalını otomatik açar
        public IActionResult MessageDetail(int id)
        {
            return RedirectToAction("MessageList", new { open = id });
        }

        // Modal açılınca çağrılır. Mesaj içeriği DEĞİŞTİRİLMEZ, sadece okundu bilgisi güncellenir.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null) return NotFound();

            if (!value.IsRead)
            {
                value.IsRead = true;
                _context.SaveChanges();
            }
            return Json(new { success = true, unread = _context.Messages.Count(x => !x.IsRead) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsUnread(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null && value.IsRead)
            {
                value.IsRead = false;
                _context.SaveChanges();
                TempData["Success"] = "Mesaj okunmadı olarak işaretlendi.";
            }
            return RedirectToAction("MessageList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Mesaj silindi.";
            }
            return RedirectToAction("MessageList");
        }
    }
}
