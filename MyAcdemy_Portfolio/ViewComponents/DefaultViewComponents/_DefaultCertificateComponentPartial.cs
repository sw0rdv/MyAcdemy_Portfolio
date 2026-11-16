using Microsoft.AspNetCore.Mvc;
using MyAcdemy_Portfolio.Context;

namespace MyAcdemy_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultCertificateComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultCertificateComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Certificates.OrderByDescending(x => x.Year).ToList();
            return View(values);
        }
    }
}
