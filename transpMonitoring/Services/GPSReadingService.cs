using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class GPSReadingService : IGPSReadingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GPSReadingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        public string Create(GPSReading gPSReading)
        {
            if (gPSReading.GPSDataId == 0)
            { return "Указанная GPS датчик не найден"; }

            GPSData? gPSData = _unitOfWork.GPSData.Get(u => u.Id == gPSReading.GPSDataId);

            if (gPSData == null) { return "Указанный GPS датчик не найден"; }

            _unitOfWork.GPSReading.Add(gPSReading);
            _unitOfWork.Save();
            return "Хранилище данных с GPS создано";
        }

        public string Delete(GPSReading gPSReading)
        {
            _unitOfWork.GPSReading.Delete(gPSReading);
            _unitOfWork.Save();
            return "Запись успешно удалёна";
        }

        public GPSReading? Get(int id)
        {
            return _unitOfWork.GPSReading.Get(u => u.Id == id);
        }

        public IEnumerable<GPSReading> GetAll()
        {
            return _unitOfWork.GPSReading.GetAll();
        }
        public IEnumerable<GPSReading> GetFirstN(int? N = 10)
        {
            return _unitOfWork.GPSReading.GetAll().Take(10);
        }

        public string Update(GPSReading gPSReading)
        {
            if (gPSReading.GPSDataId == 0)
            { return "Указанная GPS датчик не найден"; }

            GPSData? gPSData = _unitOfWork.GPSData.Get(u => u.Id == gPSReading.GPSDataId);

            if (gPSData == null) { return "Указанный GPS датчик не найден"; }

            _unitOfWork.GPSReading.Update(gPSReading);
            _unitOfWork.Save();
            return "Запись обновлена";
        }
    }
}
