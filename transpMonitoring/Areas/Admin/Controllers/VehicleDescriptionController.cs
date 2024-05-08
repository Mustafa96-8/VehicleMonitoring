using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

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
            VehicleDescriptionVM vehicleDescriptionVM = _vehicleDescriptionService.CreateVM(vehicleDescription);
            if (id == null || id == 0)
            {
                //create
                return View(vehicleDescriptionVM);
            }
            else
            {
                return View(_vehicleDescriptionService.CreateVM(_vehicleDescriptionService.Get((int)id)));
            }
        }
        [HttpPost]
        public IActionResult Upsert(VehicleDescriptionVM vehicleDescriptionVM)
        {
            if (ModelState.IsValid)
            {
                if (vehicleDescriptionVM.VehicleDescription.Id == 0)
                {
                    TempData["success"] = _vehicleDescriptionService.Create(vehicleDescriptionVM.VehicleDescription);
                }
                else
                {
                    TempData["success"] = _vehicleDescriptionService.Update(vehicleDescriptionVM.VehicleDescription);
                }
                return RedirectToAction("Index");
            }
            else
            {

                return View(vehicleDescriptionVM);
            }
        }
    }
}
