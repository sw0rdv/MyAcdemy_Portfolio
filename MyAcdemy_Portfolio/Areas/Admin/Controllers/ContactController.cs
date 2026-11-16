using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UpdateContact()
        {
            var value = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            if (contact.ContactId == 0)
                _context.Contacts.Add(contact);   
            else
                _context.Contacts.Update(contact);

            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgileri güncellendi.";
            return RedirectToAction("UpdateContact");
        }
    }
}
