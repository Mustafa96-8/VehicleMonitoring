using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;

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
        public IActionResult Index()
        {
            return View(_driverService.GetAll());
        }

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
        [HttpPost, ActionName("Delete")]
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

        public IActionResult Upsert(int? id)
        {
            Driver driver = new();
            if (id == null || id == 0)
            {
                //create
                return View(driver);
            }
            else
            {
                return View(_driverService.Get((int)id));
            }
        }
        [HttpPost]
        public IActionResult Upsert(Driver driver)
        {
            if (ModelState.IsValid)
            {
                if (driver.Id == 0)
                {
                    TempData["succes"]= _driverService.Create(driver);
                }
                else
                {
                    TempData["succes"]= _driverService.Update(driver);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(driver);
            }
        }
    }
}
