using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

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
            return View(_vehicleService.GetAll());
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
            Vehicle vehicle = new();
            if (id == null || id == 0)
            {
                return View(vehicle);
            }
            else
            {
                return View(_vehicleService.Get((int)id));
            }
        }
        [HttpPost]
        public IActionResult Upsert(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (vehicle.Id == 0)
                {
                    TempData["succes"] = _vehicleService.Create(vehicle);
                }
                else
                {
                    TempData["succes"] = _vehicleService.Update(vehicle);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(vehicle);
            }
        }
    }
}
