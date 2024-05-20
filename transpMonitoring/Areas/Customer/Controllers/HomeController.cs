using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Extensions;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;
        private readonly IReportService _reportService;
        public HomeController(ILogger<HomeController> logger,IUserService userService,IVehicleService vehicleService,IReportService reportService, ISensorService sensorService)
        {
            _logger = logger;
            _userService = userService;
            _vehicleService = vehicleService;
            _reportService = reportService;
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

        public IActionResult VehiclePage(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Vehicle? vehicle = _vehicleService.Get((int)id);
            if (vehicle == null) { return NotFound(); }
            return View(vehicle);
        }
        

        [HttpPost]
        public IActionResult _Vehicle(int id)
        {
            Vehicle? vehicle = _vehicleService.Get(id);

            if (vehicle == null) { return NotFound(); }
            _reportService.ReportHandler.GenerateReport(id);
            Report? report = _reportService.GetByVehicleId(id).OrderBy(u=>u.CreationTime).LastOrDefault();

            HomePartialVM homePartialVM = new HomePartialVM(report,vehicle);

            return PartialView(homePartialVM);
        }

        public IActionResult ReportPage(int vehicleId)
        {
            _reportService.ReportHandler.GenerateReport(vehicleId);
            List<Report> reports = _reportService.GetByVehicleId(vehicleId).OrderBy(u => u.CreationTime).ToList();

            return View(reports);
        }
        [HttpPost]
        public IActionResult _Report(int id)
        {
            Report? report = _reportService.Get(id);
            return PartialView(report);
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
