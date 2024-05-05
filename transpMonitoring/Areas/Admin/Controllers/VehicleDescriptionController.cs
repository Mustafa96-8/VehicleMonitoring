using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class VehicleDescriptionController : BaseController
    {
        readonly private IVehicleDescriptionService _vehicleDescriptionService;
        public VehicleDescriptionController(IVehicleDescriptionService vehicleDescriptionService)
        {
            _vehicleDescriptionService = vehicleDescriptionService;
        }
        public IActionResult Index()
        {
            return View(_vehicleDescriptionService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            VehicleDescription? vehicleDescription = _vehicleDescriptionService.Get((int)id);
            if (vehicleDescription == null) { return NotFound(); }
            return View(vehicleDescription);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            VehicleDescription? driverFromDb = _vehicleDescriptionService.Get((int)id);
            if (driverFromDb == null) { return NotFound(); }
            return View(driverFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            VehicleDescription? vehicleDescription = _vehicleDescriptionService.Get((int)id);
            if (vehicleDescription == null)
            {
                return NotFound();
            }

            TempData["success"] = _vehicleDescriptionService.Delete(vehicleDescription);
            return RedirectToAction("Index");
        }

        public IActionResult Upsert(int? id)
        {
            VehicleDescription vehicleDescription = new();
            if (id == null || id == 0)
            {
                //create
                return View(vehicleDescription);
            }
            else
            {
                return View(_vehicleDescriptionService.Get((int)id));
            }
        }
        [HttpPost]
        public IActionResult Upsert(VehicleDescription vehicleDescription)
        {
            if (ModelState.IsValid)
            {
                if (vehicleDescription.Id == 0)
                {
                    TempData["succes"] = _vehicleDescriptionService.Create(vehicleDescription);
                }
                else
                {
                    TempData["succes"] = _vehicleDescriptionService.Update(vehicleDescription);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(vehicleDescription);
            }
        }
    }
}
