using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VehicleMonitoring.mvc.Controllers
{
    public class BaseController : Controller
    {
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
