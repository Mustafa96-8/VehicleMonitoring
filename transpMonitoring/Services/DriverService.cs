using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DriverService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(Driver driver)
        {
            if(driver.VehicleId!=null)
            {
                Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == driver.VehicleId);
                if (vehicle == null) { return "Указанный водитель не найден"; }
                vehicle.Driver = driver;
                vehicle.DriverId = driver.Id;
                _unitOfWork.Vehicle.Update(vehicle);
            }
            _unitOfWork.Driver.Add(driver);
            _unitOfWork.Save();
            return "Водитель успешно создан";
        }

        public string Delete(Driver driver)
        {
            _unitOfWork.Driver.Delete(driver);
            _unitOfWork.Vehicle.Get(u=>u.Id==driver.VehicleId).DriverId=null;
            _unitOfWork.Save();
            return "Водитель успешно удалён";
        }

        public Driver? Get(int id)
        {
            return _unitOfWork.Driver.Get(u => u.Id == id);
        }

        public IEnumerable<Driver> GetAll()
        {
            return _unitOfWork.Driver.GetAll().ToList();
        }

        public string Update(Driver driver)
        {
            if (driver.VehicleId != null)
            {
                Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == driver.VehicleId);
                if (vehicle == null) { return "Указанный водитель не найден"; }
                vehicle.Driver = driver;
                vehicle.DriverId = driver.Id;
                _unitOfWork.Vehicle.Update(vehicle);
            }
            _unitOfWork.Driver.Update(driver);
            _unitOfWork.Save();
            return "Информация о водителе успешно обновлёна";
        }

        public DriverVM CreateVM(Driver driver,bool isModifyFromVehicle)
        {
            DriverVM driverVM = new DriverVM
            {
                Driver = driver,
                isModifyFromVehicle= isModifyFromVehicle,
                VehicleList = _unitOfWork.Vehicle.GetAll().Where(u=>u.DriverId==null||u.DriverId==driver.Id).Select(u => new SelectListItem
                {
                    Text = u.StateRegisterNumber + " | " + u.Model,
                    Value = u.Id.ToString(),
                }),
            };
            return driverVM;
        }
    }
}
