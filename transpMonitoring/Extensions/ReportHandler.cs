using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.Domain.Entities;
using System.Configuration;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services;
using System.Diagnostics;
using VehicleMonitoring.Domain.Repository;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Extensions
{
    public class ReportHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool AreThereNewSensorValues(List<Sensor> sensors,Report? LastReport)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            if (LastReport== null) return true;
            List<SensorValue> lastValues=new List<SensorValue>();
            foreach (Sensor sensor in sensors)
            {
                SensorValue? sensorValue = _unitOfWork.SensorValue.GetAll().Where(u => u.SensorId == sensor.Id).OrderBy(u => u.CreationTime).LastOrDefault();
                if (sensorValue != null) { lastValues.Add(sensorValue); }
            }
            var newSensorValues = lastValues.Where(u => u.CreationTime > LastReport.CreationTime).ToList();
            stopwatch1.Stop();
            Console.WriteLine("time of AreThereNewSensorValues = " + stopwatch1.ElapsedMilliseconds);
            if (newSensorValues.Count > 0)
            {
                return true;
            }
            return false;
        }
        
        public void GenerateReport(int vehicleId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Report? LastReport = _unitOfWork.Report.GetAll().Where(u => u.VehicleId == vehicleId).OrderBy(u => u.CreationTime).LastOrDefault();
            
            Report report = new Report
            {
                VehicleId = vehicleId
            };
            if (LastReport!=null &&(report.CreationTime -LastReport.CreationTime).Seconds < 15) 
            {
                stopwatch.Stop();
                return; 
            }

            List<Sensor> sensors = _unitOfWork.Sensor.GetAll().Where(u => u.VehicleId == vehicleId).ToList();
            if (!AreThereNewSensorValues(sensors, LastReport))
            {
                return;
            }

            _unitOfWork.Report.Add(report);
            _unitOfWork.Save();

            List<Message> messages = new List<Message>();

            foreach (Sensor sensor in sensors)
            {
                ( string content, int grade)? result =null;
                SensorValue? sensorValue = _unitOfWork.SensorValue.GetAll().Where(u=>u.SensorId==sensor.Id).OrderBy(u=>u.CreationTime).LastOrDefault();
                if (sensorValue==null)
                {
                    break;
                }
                SensorType sensorType = _unitOfWork.SensorType.Get(u=>u.Id ==sensor.SensorTypeId);
                switch (sensorType.Name)
                {
                    case "Standart":
                        result = Calculate.StandartSensorCalc(sensorValue.Value, sensor.ParametrUpper, sensor.ParametrLower );
                        break;
                    case "Fuel Level":
                        bool ignition = true; 
                        List<SensorValue> sensorValues = _unitOfWork.SensorValue.GetAll().Where(u => u.SensorId == sensor.Id).OrderBy(u => u.CreationTime).Take(5).ToList();
                        
                        SensorType? ignitionType = _unitOfWork.SensorType.GetAll().FirstOrDefault(u => u.Name == "Ignition");
                        Sensor? ignitionSensor = sensors.FirstOrDefault(u => u.SensorTypeId == ignitionType.Id);
                        if (ignitionSensor != null)
                        {
                            SensorValue? ignitionValue = _unitOfWork.SensorValue.GetAll()
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
                    _unitOfWork.Message.Add(message);
                    messages.Add(message);
                }
            }
            report.Messages = messages;
            _unitOfWork.Report.Update(report);
            _unitOfWork.Save();
            stopwatch.Stop();
            Console.WriteLine("time of GenerateReport = " + stopwatch.ElapsedMilliseconds);
            return;
        }

    }
}
