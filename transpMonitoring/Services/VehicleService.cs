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
        public IEnumerable<VehicleVM> GetAllVM()
        {
            IEnumerable<Vehicle> vehicleList =  _unitOfWork.Vehicle.GetAll().ToList();
            List<VehicleVM> vehicleVMList = new ();
            foreach (Vehicle item in vehicleList)
            {
                vehicleVMList.Add(CreateVM(item));
            }

            return vehicleVMList;
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
            IEnumerable<VehicleDescription> descriptions = vehicleDescriptionService.GetByVehicleId(vehicle.Id);
            if (descriptions != null) 
            {
                foreach (var item in descriptions)
                {
                    vehicle.Descriptions.Add(item);
                }
            }
            vehicle.User = _unitOfWork.User.Get(u => u.Id == vehicle.UserId);
            _unitOfWork.Vehicle.Add(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно создан";
        }
        

        public string Update(Vehicle vehicle)
        {
            VehicleDescriptionService vehicleDescriptionService = new(_unitOfWork);
            vehicle.Descriptions.Clear();
            IEnumerable<VehicleDescription> descriptions = vehicleDescriptionService.GetByVehicleId(vehicle.Id);
            if (descriptions != null)
            {
                foreach (var item in descriptions)
                {
                    vehicle.Descriptions.Add(item);
                }
            }
            vehicle.User = _unitOfWork.User.Get(u => u.Id==vehicle.UserId);
            vehicle.Driver = _unitOfWork.Driver.Get(u => u.VehicleId == vehicle.Id);
            if (vehicle.Driver == null) 
            { 
                vehicle.DriverId = null; 
            } 
            else
            {
                vehicle.DriverId = vehicle.Driver.Id;
            }
            _unitOfWork.Vehicle.Update(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно Обновлён";
        }

        public string Delete(Vehicle vehicle)
        {
            VehicleDescriptionService vehicleDescriptionService = new(_unitOfWork);
            foreach (var item in vehicleDescriptionService.GetByVehicleId(vehicle.Id))
            {
                _unitOfWork.VehicleDescription.Delete(item);
            }
            _unitOfWork.Vehicle.Delete(vehicle);
            _unitOfWork.Save();
            return "Транспорт успешно удалён";
        }

        public VehicleVM CreateVM(Vehicle vehicle)
        {
            
            VehicleVM VehicleVM = new VehicleVM
            {
                Vehicle = vehicle,
                UserList = _unitOfWork.User.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FirstName + " | " + u.LastName,
                    Value = u.Id.ToString(),
                }),
                Driver = _unitOfWork.Driver.Get(u=> u.VehicleId == vehicle.Id)
            };
            return VehicleVM;
        }
        
    }
}
