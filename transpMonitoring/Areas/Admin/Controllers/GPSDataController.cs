using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class GPSDataController : BaseController
    {
        private readonly IGPSDataService _gpsdataService;
        public GPSDataController(IGPSDataService gpsdataService)
        {
            _gpsdataService = gpsdataService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_gpsdataService.GetAll());
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            GPSData? gPSData = _gpsdataService.Get((int)id);
            if (gPSData == null) { return NotFound(); }
            return View(gPSData);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            GPSData? gPSData = _gpsdataService.Get((int)id);
            if (gPSData == null) { return NotFound(); }
            return View(gPSData);
        }
        [HttpDelete, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            GPSData? gPSData = _gpsdataService.Get((int)id);
            if (gPSData == null)
            {
                return NotFound();
            }

            TempData["success"] = _gpsdataService.Delete(gPSData);
            return RedirectToAction("Index");
        }
        public IActionResult Upsert(int? id)
        {
            GPSDataVM gPSDataVM;
            if (id == null || id == 0)
            {
                GPSData gPSData = new();
                gPSDataVM = _gpsdataService.CreateVM(gPSData);
            }
            else
            {
                GPSData? gPSData = _gpsdataService.Get((int)id);
                if (gPSData == null)
                {
                    return NotFound();
                }
                gPSDataVM = _gpsdataService.CreateVM(gPSData);

            }
            return View(gPSDataVM);
        }
        [HttpPost]
        public IActionResult Upsert(GPSDataVM gPSDataVM)
        {
            if (ModelState.IsValid)
            {
                if (gPSDataVM.GPSData.Id == 0)
                {
                    TempData["success"] = _gpsdataService.Create(gPSDataVM.GPSData);
                }
                else
                {
                    TempData["success"] = _gpsdataService.Update(gPSDataVM.GPSData);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(_gpsdataService.CreateVM(gPSDataVM.GPSData));
            }
        }
    }
}
