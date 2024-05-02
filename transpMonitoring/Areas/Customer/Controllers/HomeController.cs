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

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public IActionResult Index()
        {
            if(CurrentUserId == 0)
            {
                return RedirectToAction("logout", "Account", new { area = "Customer" });
                    //asp - controller = "Account" asp - action = "Logout"
            }
            User user = _unitOfWork.User.Get(u => u.Id == CurrentUserId);
            if (user == null)
            {
                return RedirectToAction("logout", "Account", new { area = "Customer" });
                //asp - controller = "Account" asp - action = "Logout"
            }
            return View(user);
        }

        [Authorize(Roles = "admin")]
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
