using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class VehicleController : BaseController
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        public IActionResult Index()
        {
            return View(_vehicleService.GetAllVM());
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Vehicle? vehicle = _vehicleService.Get((int)id);
            if (vehicle == null) { return NotFound(); }
            return View(vehicle);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Vehicle? vehicle = _vehicleService.Get((int)id);
            if (vehicle == null) { return NotFound(); }
            return View(vehicle);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            Vehicle? vehicle = _vehicleService.Get((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }
            TempData["success"] = _vehicleService.Delete(vehicle);
            return RedirectToAction("Index");
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                Vehicle vehicle = new();
                VehicleVM vehicleVM = _vehicleService.CreateVM(vehicle);
                return View(vehicleVM);
            }
            else
            {
                return View(_vehicleService.CreateVM(_vehicleService.Get((int)id)));
            }
        }
        [HttpPost]
        public IActionResult Upsert(VehicleVM vehicleVM)
        {
            if (ModelState.IsValid)
            {
                if (vehicleVM.Vehicle.Id == 0)
                {
                    TempData["success"] = _vehicleService.Create(vehicleVM.Vehicle);
                }
                else
                {
                    TempData["success"] = _vehicleService.Update(vehicleVM.Vehicle);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(_vehicleService.CreateVM(vehicleVM.Vehicle));
            }
        }
        
    }
}
