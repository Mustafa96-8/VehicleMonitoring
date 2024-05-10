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
        public IEnumerable<SensorType> GetAll()
        {
            return _unitOfWork.SensorType.GetAll().ToList();
        }

        public SensorType? Get(int id) 
        {
            return _unitOfWork.SensorType.Get(u => u.Id == id);
        }

        public string Delete(SensorType sensorType)
        {
            _unitOfWork.SensorType.Delete(sensorType);
            _unitOfWork.Save();
            return "Категория датчиков успешно удалена";
        }

        public string Create(SensorType sensorType)
        {
            throw new NotImplementedException();
        }

        public string Update(SensorType obj)
        {
            throw new NotImplementedException();
        }
    }
}
