using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Services
{
    public class SensorValueService : ISensorValueService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SensorValueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(SensorValue sensorValue)
        {
            if (sensorValue.SensorId == 0)
            {
                return "Указанный датчик не найден";
            }
            Sensor? sensor = _unitOfWork.Sensor.Get(u => u.Id == sensorValue.SensorId);
            if (sensor == null)
            {
                return "Указанный датчик не найден";
            }
            sensorValue.Sensor = sensor;
            _unitOfWork.SensorValue.Add(sensorValue);
            _unitOfWork.Save();
            return "Запись создана";
        }

        public string Delete(SensorValue sensorValue)
        {
            _unitOfWork.SensorValue.Delete(sensorValue);
            _unitOfWork.Save();
            return "Запись успешно удалёна";
        }

        public SensorValue? Get(int id)
        {
            return _unitOfWork.SensorValue.Get(u => u.Id == id);
        }

        public IEnumerable<SensorValue> GetAll()
        {
            return _unitOfWork.SensorValue.GetAll();
        }

        public string Update(SensorValue sensorValue)
        {
            if (sensorValue.SensorId == 0)
            {
                return "Указанный датчик не найден";
            }
            Sensor? sensor = _unitOfWork.Sensor.Get(u => u.Id == sensorValue.SensorId);
            if (sensor == null)
            {
                return "Указанный датчик не найден";
            }
            sensorValue.Sensor = sensor;
            _unitOfWork.SensorValue.Update(sensorValue);
            _unitOfWork.Save();
            return "Запись создана";
        }
    }
}
