using Microsoft.AspNetCore.Mvc;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Services
{
    public class SensorTypeService :ISensorTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SensorTypeService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SensorType> GetSensorTypes()
        {
            return _unitOfWork.SensorType.GetAll().ToList();
        }

        public SensorType GetSensorType(int id) 
        {
            SensorType? sensorTypeFromDb = _unitOfWork.SensorType.Get(u=>u.Id==id);
            if (sensorTypeFromDb == null)
            {
                return null;
            }
            return sensorTypeFromDb;
        }

        public string DeleteSensorType(SensorType sensorType)
        {
            _unitOfWork.SensorType.Delete(sensorType);
            _unitOfWork.Save();
            return "Категория датчиков успешно удалена";
        }
    }
}
