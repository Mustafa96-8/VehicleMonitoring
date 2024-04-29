using Microsoft.AspNetCore.Mvc;

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
