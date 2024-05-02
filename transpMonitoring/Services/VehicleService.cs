using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;

namespace VehicleMonitoring.mvc.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _unitOfWork.Vehicle.GetAll().ToList();
        }

        public Vehicle GetVehicle(int id)
        {
            Vehicle? sensorTypeFromDb = _unitOfWork.Vehicle.Get(u => u.Id == id);
            if (sensorTypeFromDb == null)
            {
                return null;
            }
            return sensorTypeFromDb;
        }

        public string DeleteVehicle(Vehicle vehicle)
        {
            _unitOfWork.Vehicle.Delete(vehicle);
            _unitOfWork.Save();
            return "Категория датчиков успешно удалена";
        }
    }
}
