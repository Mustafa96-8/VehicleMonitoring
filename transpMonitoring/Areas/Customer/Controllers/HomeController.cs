using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Diagnostics;
using System.Security.Claims;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger,IUserService userService )
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if(CurrentUserId == 0)
            {
                return RedirectToAction("logout", "Account", new { area = "Customer" });
            }
            User? user = _userService.Get(CurrentUserId);
            if (user == null)
            {
                return RedirectToAction("logout", "Account", new { area = "Customer" });
            }
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
