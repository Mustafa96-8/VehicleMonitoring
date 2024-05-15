using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class SensorService : ISensorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SensorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(Sensor sensor)
        {
            if (sensor.VehicleId == 0)
            { 
                return "Указанный Транспорт не найден"; 
            }
            if (sensor.SensorTypeId == 0) 
            { 
                return "Указанный тип датчика не найден"; 
            }
            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == sensor.VehicleId);
            SensorType? sensorType = _unitOfWork.SensorType.Get(u => u.Id == sensor.SensorTypeId);
            if (vehicle == null) 
            { 
                return "Указанный Транспорт не найден"; 
            }
            if (sensorType == null) 
            { 
                return "Указанный тип датчика не найден"; 
            }
            sensor.Vehicle = vehicle;
            sensor.SensorType = sensorType;
            _unitOfWork.Sensor.Add(sensor);
            _unitOfWork.Save();
            return "Датчик создан";
        }

        public string Delete(Sensor sensor)
        {
            _unitOfWork.Sensor.Delete(sensor);
            _unitOfWork.Save();
            return "Датчик успешно удалён";
        }

        public Sensor? Get(int id)
        {
            return _unitOfWork.Sensor.Get(u => u.Id == id);
        }

        public IEnumerable<Sensor> GetAll()
        {
            return _unitOfWork.Sensor.GetAll().ToList();
        }

        public string Update(Sensor sensor)
        {
            if (sensor.VehicleId == 0)
            {
                return "Указанный Транспорт не найден";
            }
            if (sensor.SensorTypeId == 0)
            {
                return "Указанный тип датчика не найден";
            }
            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == sensor.VehicleId);
            SensorType? sensorType = _unitOfWork.SensorType.Get(u => u.Id == sensor.SensorTypeId);
            if (vehicle == null)
            {
                return "Указанный Транспорт не найден";
            }
            if (sensorType == null)
            {
                return "Указанный тип датчика не найден";
            }
            sensor.Vehicle = vehicle;
            sensor.SensorType = sensorType;
            _unitOfWork.Sensor.Update(sensor);
            _unitOfWork.Save();
            return "Датчик создан";
        }

        public SensorVM CreateVM(Sensor sensor)
        {
            SensorVM sensorVM = new SensorVM
            {
                Sensor = sensor,
                VehicleList = _unitOfWork.Vehicle.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StateRegisterNumber + " | " + u.Model,
                    Value = u.Id.ToString(),
                }),
                SensorTypeList = _unitOfWork.SensorType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

            };
            return sensorVM;
        }
    }
}
