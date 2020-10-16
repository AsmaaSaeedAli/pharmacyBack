using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers
{
    public class HomeController : PharmacyControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ui");
        }
    }
}
