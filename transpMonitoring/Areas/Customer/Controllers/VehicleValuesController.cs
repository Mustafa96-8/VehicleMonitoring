using AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Controllers;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Areas.Customer.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class VehicleValuesController : BaseController 
    {
        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;
        private readonly ISensorValueService _sensorValueService;
        private readonly ISensorService _sensorService;
        private readonly IGPSDataService _gpsDataService;
        private readonly IGPSReadingService _gpsReadingService;
        public VehicleValuesController(IUserService userService, IVehicleService vehicleService,  ISensorService sensorService, ISensorValueService sensorValueService,IGPSDataService gPSDataService,IGPSReadingService gPSReadingService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _sensorValueService = sensorValueService;
            _sensorService = sensorService;
            _gpsDataService = gPSDataService;
            _gpsReadingService = gPSReadingService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Vehicle> vehicles = _vehicleService.GetAll().Where(u=>u.UserId==CurrentUserId).ToList();
            return View(vehicles);
        }
        [HttpPost]
        public IActionResult _Sensor(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }
            
            List<Sensor> sensors = _sensorService.GetAll().Where(u=>u.VehicleId==id).ToList();

            List<(Sensor,SensorValue)> sensorValueList = new List<(Sensor,SensorValue)> ();
            
            foreach (Sensor sensor in sensors)
            {
                SensorValue lastValue= _sensorValueService.GetAll()
                    .Where(u => u.SensorId == sensor.Id)
                    .OrderBy(u => u.CreationTime)
                    .LastOrDefault();
                sensorValueList.Add(item: (sensor, lastValue));
            }
            
            return PartialView(sensorValueList);                                                              
        }


        public IActionResult SensorPage(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }

            List<Sensor> sensors = _sensorService.GetAll().Where(u => u.VehicleId == id).ToList();

            List<(Sensor, SensorValue)> sensorValueList = new List<(Sensor, SensorValue)>();

            foreach (Sensor sensor in sensors)
            {
                SensorValue lastValue = _sensorValueService.GetAll()
                    .Where(u => u.SensorId == sensor.Id)
                    .OrderBy(u => u.CreationTime)
                    .LastOrDefault();
                sensorValueList.Add(item: (sensor, lastValue));
            }

            return View(sensorValueList);
        }

        [HttpGet]
        public IActionResult SensorGraph(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }

            Sensor sensor = _sensorService.Get((int)id);

            Tuple<Sensor, List<SensorValue>> sensorValue = new Tuple<Sensor, List<SensorValue>> (sensor, _sensorValueService.GetAll().Where(u=>u.SensorId==id).OrderBy(u=>u.CreationTime).ToList());

            return View(sensorValue);
        }


        [HttpGet]
        public IActionResult MapPage(int? id)
        {
            if (id == null || id == 0) { return BadRequest(); }

            GPSData gpsData = _gpsDataService.GetbyVehicleId((int)id);

            GPSReading gPSReading = _gpsReadingService.GetAll().Where(u => u.GPSDataId == gpsData.Id).LastOrDefault();
            Tuple<GPSData, List<GPSReading>> readings = new Tuple<GPSData, List<GPSReading>>(gpsData, _gpsReadingService.GetAll().Where(u => u.GPSDataId == gpsData.Id).OrderBy(u => u.CreationTime).Reverse().ToList());
            if (readings.Item2.Count==0) {
                return NotFound();
            }
            return View(readings);
        }
    }
}
