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
            _unitOfWork.Driver.Add(driver);
            _unitOfWork.Save();
            return "Водитель успешно создан";
        }

        public string Delete(Driver driver)
        {
            _unitOfWork.Driver.Delete(driver);
            _unitOfWork.Save();
            return "Транспорт успешно удалён";
        }

        public Driver Get(int id)
        {
            Driver? driverFromDb = _unitOfWork.Driver.Get(u => u.Id == id);
            if (driverFromDb == null)
            {
                return null;
            }
            return driverFromDb;
        }

        public IEnumerable<Driver> GetAll()
        {
            return _unitOfWork.Driver.GetAll().ToList();
        }

        public string Update(Driver driver)
        {
            _unitOfWork.Driver.Update(driver);
            _unitOfWork.Save();
            return "Информация о водителе успешно Обновлён";
        }
    }
}
