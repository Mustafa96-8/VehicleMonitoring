using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class SensorTypeController : BaseController
    {
        private readonly ISensorTypeService _sensorTypeService;
        public SensorTypeController(ISensorTypeService sensorTypeService)
        {
            _sensorTypeService = sensorTypeService;
        }
        public IActionResult Index()
        {
            return View(_sensorTypeService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if(id == null||id==0) { return BadRequest(); }
            SensorType? sensorType = _sensorTypeService.Get((int)id);
            if (sensorType == null) { return NotFound(); }
            return View(sensorType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            SensorType? sensorType = _sensorTypeService.Get((int)id);
            if (sensorType == null) { return NotFound(); }
            return View(sensorType);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            SensorType? sensorType = _sensorTypeService.Get((int)id);
            if (sensorType == null)
            {
                return NotFound();
            }
            TempData["success"] = _sensorTypeService.Delete(sensorType);
            return RedirectToAction("Index");
        }
        public IActionResult Upsert(int? id)
        {
            SensorType sensorType = new();
            if (id == null || id == 0)
            {
                //create
                return View(sensorType);
            }
            else
            {
                return View(_sensorTypeService.Get((int)id));
            }
        }
        [HttpPost]
        public IActionResult Upsert(SensorType sensorType)
        {
            if (ModelState.IsValid)
            {
                if (sensorType.Id == 0)
                {
                    TempData["success"] = _sensorTypeService.Create(sensorType);
                }
                else
                {
                    TempData["success"] = _sensorTypeService.Update(sensorType);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(sensorType);
            }
        }
    }
}
