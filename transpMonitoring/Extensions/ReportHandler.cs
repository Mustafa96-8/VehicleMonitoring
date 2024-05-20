using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.Domain.Entities;
using System.Configuration;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services;

namespace VehicleMonitoring.mvc.Extensions
{
    public class ReportHandler
    {
        private readonly ISensorService _sensorService;
        private readonly ISensorTypeService _sensorTypeService;
        private readonly ISensorValueService _sensorValueService;
        private readonly IReportService _reportService;
        private readonly IMessageService _messageService;

        public ReportHandler(ISensorService sensorService,ISensorValueService sensorValueService,ISensorTypeService sensorTypeService, IReportService reportService, IMessageService messageService )
        {
            _sensorService = sensorService;
            _sensorValueService = sensorValueService;
            _sensorTypeService = sensorTypeService;
            _reportService = reportService;
            _messageService = messageService;
        }
        public ReportHandler(IUnitOfWork unitOfWork)
        {
            _sensorService = new SensorService(unitOfWork);
            _sensorValueService = new SensorValueService(unitOfWork);
            _sensorTypeService = new SensorTypeService(unitOfWork);
            _reportService = new ReportService(unitOfWork);
            _messageService = new MessageService(unitOfWork);
        }

        private bool AreThereNewSensorValues(List<Sensor> sensors,Report? LastReport)
        {
            if(LastReport== null) return true;
            List<SensorValue> lastValues=new List<SensorValue>();
            foreach (Sensor sensor in sensors)
            {
                SensorValue? sensorValue = _sensorValueService.GetAll().Where(u => u.SensorId == sensor.Id).OrderBy(u => u.CreationTime).LastOrDefault();
                if (sensorValue != null) { lastValues.Add(sensorValue); }
            }
            var newSensorValues = lastValues.Where(u => u.CreationTime > LastReport.CreationTime).ToList();
            if (newSensorValues.Count > 0)
            {
                return true;
            }
            return false;
        }
        
        public void GenerateReport(int VehicleId)
        {
            Report? LastReport = _reportService.GetByVehicleId(VehicleId).OrderBy(u => u.CreationTime).LastOrDefault();
            Report report = new Report
            {
                VehicleId = VehicleId
            };
            if (LastReport!=null &&(report.CreationTime -LastReport.CreationTime).Seconds < 15) { return; }

            List<Sensor> sensors = _sensorService.GetAll().Where(u => u.VehicleId == VehicleId).ToList();
            if (!AreThereNewSensorValues(sensors, LastReport)) { return; }

            _reportService.Create(report);

            List<Message> messages = new List<Message>();

            foreach (Sensor sensor in sensors)
            {
                ( string content, int grade)? result =null;
                SensorValue? sensorValue = _sensorValueService.GetAll().Where(u=>u.SensorId==sensor.Id).OrderBy(u=>u.CreationTime).LastOrDefault();
                if (sensorValue==null)
                {
                    break;
                }
                switch (sensor.SensorType.Name)
                {
                    case "Standart":
                        result = Calculate.StandartSensorCalc(sensorValue.Value, sensor.ParametrUpper, sensor.ParametrLower );
                        break;
                    case "Fuel Level":
                        bool ignition = true; 
                        List<SensorValue> sensorValues = _sensorValueService.GetAll().Where(u => u.SensorId == sensor.Id).OrderBy(u => u.CreationTime).Take(5).ToList();
                        
                        SensorType? ignitionType = _sensorTypeService.GetAll().FirstOrDefault(u => u.Name == "Ignition");
                        Sensor? ignitionSensor = _sensorService.GetAll().FirstOrDefault(u => u.SensorTypeId == ignitionType.Id);
                        if (ignitionSensor != null)
                        {
                            SensorValue? ignitionValue = _sensorValueService.GetAll()
                             .Where(u => u.SensorId == ignitionSensor.Id)
                             .OrderBy(u => u.CreationTime)
                             .FirstOrDefault();

                            ignition = ignitionValue == null ? true : ignitionValue.Value==0?false:true ;
                        }
                        result = Calculate.FuelLevelCalc(sensorValues, ignition, sensor.ParametrUpper,5);
                        break;
                    case "Velocity":
                        result = Calculate.VelocitySensorCalc(sensorValue.Value,sensor.ParametrUpper);
                        break;
                    case "Acceleration sensor":
                        result = Calculate.AccelerationSensorСalc(sensorValue.Value, sensor.ParametrUpper);
                        break;
                }
                if (result != null)
                {
                    Message message = new Message
                    {
                        Content = result.Value.content+" в датчике " +sensor.Name,
                        Grade = result.Value.grade,
                        ReportId = report.Id,
                    };
                    _messageService.Create(message);
                    messages.Add(message);
                }
            }
            report.Messages = messages;
            _reportService.Update(report);

        }

    }
}
