using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Areas.Admin.Controllers
{
    public class SensorController : BaseController
    {
        private readonly ISensorService _sensorService;
        public SensorController(ISensorService sensorService, IVehicleService vehicleService)
        {
            _sensorService = sensorService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_sensorService.GetAll());
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Sensor? sensor = _sensorService.Get((int)id);
            if (sensor == null) { return NotFound(); }
            return View(sensor);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            Sensor? sensor = _sensorService.Get((int)id);
            if (sensor == null) { return NotFound(); }
            return View(sensor);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            Sensor? sensor = _sensorService.Get((int)id);
            if (sensor == null)
            {
                return NotFound();
            }

            TempData["success"] = _sensorService.Delete(sensor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Sensor? sensor = new();
            SensorVM sensorVM = _sensorService.CreateVM(sensor);
            if (id == null || id == 0)
            {
                //create
                return View(sensorVM);
            }
            else
            {
                sensor = _sensorService.Get((int)id);
                if (sensor == null)
                {
                    return NotFound();
                }
                return View(_sensorService.CreateVM(sensor));
            }
        }
        [HttpPost]
        public IActionResult Upsert(SensorVM sensorVM)
        {
            if (ModelState.IsValid)
            {
                if (sensorVM.Sensor.Id == 0)
                {
                    TempData["success"] = _sensorService.Create(sensorVM.Sensor);
                }
                else
                {
                    TempData["success"] = _sensorService.Update(sensorVM.Sensor);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(sensorVM);
            }
        }

    }
}
