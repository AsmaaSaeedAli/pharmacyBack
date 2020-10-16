using Microsoft.AspNetCore.Mvc;
using Pharmacy.Web.Controllers;

namespace Pharmacy.Web.Public.Controllers
{
    public class HomeController : PharmacyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}