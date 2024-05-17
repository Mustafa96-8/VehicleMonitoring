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

        public void GenerateReport(int VehicleId)
        {
            List<Sensor> sensors = _sensorService.GetAll().Where(u=>u.VehicleId== VehicleId).ToList();
            List<Message> messages = new List<Message>();
            Report report = new Report
            {
                VehicleId= VehicleId
            };
            _reportService.Create(report);
            foreach (Sensor sensor in sensors)
            {
                ( string content, int grade)? result =null;
                SensorValue? sensorValue = _sensorValueService.GetAll().Where(u=>u.SensorId==sensor.Id).OrderBy(u=>u.CreationTime).LastOrDefault();
                switch (sensor.SensorType.Name)
                {
                    case "Standart":
                        result = Calculate.StandartSensorCalc((double)sensor.ParametrUpper, (double)sensor.ParametrLower, sensorValue.Value);
                        break;
                    case "Fuel Level":
                        List<SensorValue> sensorValues = _sensorValueService.GetAll().Where(u => u.SensorId == sensor.Id).OrderBy(u => u.CreationTime).Take(5).ToList();
                        SensorType ignitionType = _sensorTypeService.GetAll().Where(u => u.Name == "Ignition").FirstOrDefault();
                        Sensor ignitionSensor = _sensorService.GetAll().Where(u => u.SensorTypeId == ignitionType.Id).FirstOrDefault();
                        SensorValue ignitionValue = _sensorValueService.GetAll().Where(u => u.SensorId == ignitionSensor.Id).OrderBy(u => u.CreationTime).FirstOrDefault();
                        bool ignition = ignitionValue.Value == 0 ? false : true;
                        result = Calculate.FuelLevelCalc(sensorValues, (double)sensor.ParametrUpper,ignition);
                        break;
                    case "Velocity":
                        result = Calculate.VelocitySensorCalc((double)sensor.ParametrUpper,sensorValue.Value);
                        break;
                    case "Acceleration sensor":
                        result = Calculate.AccelerationSensorСalc(sensorValue.Value, (double)sensor.ParametrUpper);
                        break;
                }
                if (result != null)
                {
                    Message message = new Message
                    {
                        Content = result.Value.content+" в датчике" +sensor.Name,
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
