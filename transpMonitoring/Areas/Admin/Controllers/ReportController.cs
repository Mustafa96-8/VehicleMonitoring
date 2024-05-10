using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.mvc.Controllers;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
