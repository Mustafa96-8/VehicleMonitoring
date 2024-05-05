using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;
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
        public IEnumerable<Vehicle> GetAll()
        {
            return _unitOfWork.Vehicle.GetAll().ToList();
        }

        public Vehicle Get(int id)
        {
            Vehicle? vehicleFromDb = _unitOfWork.Vehicle.Get(u => u.Id == id);
            if (vehicleFromDb == null)
            {
                return null;
            }
            return vehicleFromDb;
        }

        public string Create(Vehicle vehicle) 
        {
            VehicleDescriptionService vehicleDescriptionService = new(_unitOfWork);
            foreach (var item in vehicleDescriptionService.GetByVehicleId(vehicle.Id))
            {
                vehicle.Descriptions.Add(item);
            }
            _unitOfWork.Vehicle.Add(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно создан";
        }
        

        public string Update(Vehicle vehicle)
        {
            VehicleDescriptionService vehicleDescriptionService = new(_unitOfWork);
            vehicle.Descriptions.Clear();
            foreach (var item in vehicleDescriptionService.GetByVehicleId(vehicle.Id))
            {
                vehicle.Descriptions.Add(item);
            }
            _unitOfWork.Vehicle.Update(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно Обновлён";
        }

        public string Delete(Vehicle vehicle)
        {
            _unitOfWork.Vehicle.Delete(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно удалён";
        }
    }
}
