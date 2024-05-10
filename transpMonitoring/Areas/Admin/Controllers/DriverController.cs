using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class DriverController : BaseController
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_driverService.GetAll());
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Driver? driverFromDb =_driverService.Get((int)id);
            if (driverFromDb == null) { return NotFound(); }
            return View(driverFromDb);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Driver? driverFromDb = _driverService.Get((int)id);
            if (driverFromDb == null) { return NotFound(); }
            return View(driverFromDb);
        }
        [HttpDelete, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            Driver? driverFromDb = _driverService.Get((int)id);
            if (driverFromDb == null)
            {
                return NotFound();
            }

            TempData["success"] = _driverService.Delete(driverFromDb);
            return RedirectToAction("Index");
        }

        public IActionResult Upsert(int? id,int? VehicleId)
        {
            DriverVM driverVM;
            if (id == null || id == 0)
            {
                Driver driver = new() { VehicleId = VehicleId };
                driverVM = _driverService.CreateVM(driver, VehicleId != null);
            }
            else
            {
                Driver? driver = _driverService.Get((int)id);
                if (driver == null)
                {
                    return NotFound();
                }
                driverVM = _driverService.CreateVM(driver, VehicleId != null);

            }
            return View(driverVM);
        }
        [HttpPost]                        
        public IActionResult Upsert(DriverVM driverVM)
        {
            if (ModelState.IsValid)
            {
                if (driverVM.Driver.Id == 0)
                {
                    TempData["success"]= _driverService.Create(driverVM.Driver);
                }
                else
                {
                    TempData["success"]= _driverService.Update(driverVM.Driver);
                }
                if (driverVM.isModifyFromVehicle)
                {
                    return RedirectToAction("Upsert", "Vehicle", new { area = "Admin" ,Id=driverVM.Driver.VehicleId});
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(_driverService.CreateVM(driverVM.Driver, driverVM.isModifyFromVehicle));
            }
        }
        
    }
}
