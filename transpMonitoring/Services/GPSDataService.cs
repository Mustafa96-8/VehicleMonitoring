using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class GPSDataService : IGPSDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GPSDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(GPSData gPSData)
        {
            if (gPSData.VehicleId == 0)
            { return "Указанный Транспорт не найден"; }

            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == gPSData.VehicleId);

            if (vehicle == null) { return "Указанный GPS логер не найден"; }

            vehicle.GPSData = gPSData;
            vehicle.GPSDataId = gPSData.Id;
            _unitOfWork.Vehicle.Update(vehicle);
            _unitOfWork.GPSData.Add(gPSData);
            _unitOfWork.Save();
            return "Хранилище данных с GPS создано";
        }

        public string Delete(GPSData gPSData)
        {
            _unitOfWork.GPSData.Delete(gPSData);
            _unitOfWork.Vehicle.Get(u => u.Id == gPSData.Id).GPSDataId= null;
            _unitOfWork.Save();
            return "GPS логер успешно удалён";
        }

        public GPSData? Get(int id)
        {
            return _unitOfWork.GPSData.Get(u => u.Id == id);
        }
        public GPSData? GetbyVehicleId(int vehicleId)
        {
            return _unitOfWork.GPSData.Get(u => u.VehicleId == vehicleId);
        }

        public IEnumerable<GPSData> GetAll()
        {
            return _unitOfWork.GPSData.GetAll().ToList();
        }

        public string Update(GPSData gPSData)
        {
            if (gPSData.VehicleId == 0)
            { return "Указанный GPS логер не найден"; }

            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == gPSData.VehicleId);

            if (vehicle == null) { return "Указанный GPS логер не найден"; }

            vehicle.GPSData = gPSData;
            vehicle.GPSDataId = gPSData.Id;
            _unitOfWork.Vehicle.Update(vehicle); 
            _unitOfWork.GPSData.Update(gPSData);
            _unitOfWork.Save();
            return "Хранилище данных с GPS изменено";
            
            
        }
        public GPSDataVM CreateVM(GPSData gPSData)
        {
            GPSDataVM gPSDataVM = new GPSDataVM
            {
                GPSData = gPSData,
                VehicleList = _unitOfWork.Vehicle.GetAll().Where(u => u.GPSDataId == null || u.GPSDataId== gPSData.Id).Select(u => new SelectListItem
                {
                    Text = u.StateRegisterNumber + " | " + u.Model,
                    Value = u.Id.ToString(),
                }),

            };
            return gPSDataVM;
        }

    }
}
