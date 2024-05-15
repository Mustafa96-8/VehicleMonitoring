using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace GPSReadingMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class GPSReadingController : BaseController
    {
        private readonly IGPSReadingService _gPSReadingService;
        public GPSReadingController(IGPSReadingService gPSReadingService)
        {
            _gPSReadingService = gPSReadingService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_gPSReadingService.GetAll());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            GPSReading? gPSReading = _gPSReadingService.Get((int)id);
            if (gPSReading == null) { return NotFound(); }
            return View(gPSReading);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            GPSReading? gPSReading = _gPSReadingService.Get((int)id);
            if (gPSReading == null) { return NotFound(); }
            return View(gPSReading);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            GPSReading? gPSReading = _gPSReadingService.Get((int)id);
            if (gPSReading == null)
            {
                return NotFound();
            }
            TempData["success"] = _gPSReadingService.Delete(gPSReading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                GPSReading gPSReading = new();
                return View(gPSReading);
            }
            else
            {
                GPSReading? gPSReading = _gPSReadingService.Get((int)id);
                if (gPSReading == null)
                {
                    return NotFound();
                }
                return View(gPSReading);
            }
        }
        [HttpPost]
        public IActionResult Upsert(GPSReading gPSReading)
        {
            if (ModelState.IsValid)
            {
                if (gPSReading.Id == 0)
                {
                    TempData["success"] = _gPSReadingService.Create(gPSReading);
                }
                else
                {
                    TempData["success"] = _gPSReadingService.Update(gPSReading);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(gPSReading);
            }
        }
    }
}
