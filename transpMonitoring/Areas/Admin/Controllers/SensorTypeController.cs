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
            return View(_sensorTypeService.GetSensorTypes());
        }

        public IActionResult Details(int? id)
        {
            if(id == null||id==0) { return BadRequest(); }
            SensorType? sensorType = _sensorTypeService.GetSensorType((int)id);
            if (sensorType == null) { return NotFound(); }
            return View(sensorType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            SensorType? sensorType = _sensorTypeService.GetSensorType((int)id);
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
            SensorType? sensorType = _sensorTypeService.GetSensorType((int)id);
            if (sensorType == null)
            {
                return NotFound();
            }
            TempData["success"] = _sensorTypeService.DeleteSensorType(sensorType); ;
            return RedirectToAction("Index");
        }
    }
}
