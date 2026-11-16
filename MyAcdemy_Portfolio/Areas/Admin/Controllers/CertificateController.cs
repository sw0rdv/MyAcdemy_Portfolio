using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;
using MyAcdemy_Portfolio.Entities;

namespace MyAcdemy_Portfolio.Areas.Admin.Controllers
{
    // Sertifikalarım yönetimi
    [Area("Admin")]
    public class CertificateController : Controller
    {
        private readonly PortfolioContext _context;

        public CertificateController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("CertificateList");
        }

        public IActionResult CertificateList()
        {
            var values = _context.Certificates.OrderByDescending(x => x.Year).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCertificate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }

            _context.Certificates.Add(certificate);
            _context.SaveChanges();
            return RedirectToAction("CertificateList");
        }

        public IActionResult DeleteCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null) return RedirectToAction("CertificateList");

            _context.Certificates.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("CertificateList");
        }

        [HttpGet]
        public IActionResult UpdateCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null) return RedirectToAction("CertificateList");

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }

            _context.Certificates.Update(certificate);
            _context.SaveChanges();
            return RedirectToAction("CertificateList");
        }
    }
}
