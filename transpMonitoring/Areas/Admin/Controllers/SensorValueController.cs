using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class SensorValueController : BaseController
    {
        private readonly ISensorValueService _SensorValueService;
        public SensorValueController(ISensorValueService sensorValueService)
        {
            _SensorValueService = sensorValueService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_SensorValueService.GetAll());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            SensorValue? sensorValue = _SensorValueService.Get((int)id);
            if (sensorValue == null) { return NotFound(); }
            return View(sensorValue);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            SensorValue? sensorValue = _SensorValueService.Get((int)id);
            if (sensorValue == null) { return NotFound(); }
            return View(sensorValue);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            SensorValue? sensorValue = _SensorValueService.Get((int)id);
            if (sensorValue == null)
            {
                return NotFound();
            }
            TempData["success"] = _SensorValueService.Delete(sensorValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                SensorValue sensorValue = new();
                return View(sensorValue);
            }
            else
            {
                SensorValue? sensorValue = _SensorValueService.Get((int)id);
                if (sensorValue == null)
                {
                    return NotFound();
                }
                return View(sensorValue);
            }
        }
        [HttpPost]
        public IActionResult Upsert(SensorValue sensorValue)
        {
            if (ModelState.IsValid)
            {
                if (sensorValue.Id == 0)
                {
                    TempData["success"] = _SensorValueService.Create(sensorValue);
                }
                else
                {
                    TempData["success"] = _SensorValueService.Update(sensorValue);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(sensorValue);
            }
        }
    }
}
