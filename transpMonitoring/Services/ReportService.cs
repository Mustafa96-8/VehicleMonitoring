using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;
using VehicleMonitoring.mvc.Services.IServices;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Create(Report report)
        {
            if (report.VehicleId == null || report.VehicleId == 0)
            {
                return "Указанное ТС не найдено";
            }
            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == report.VehicleId);
            if (vehicle == null) 
            { 
                return "Указанное ТС не найдено"; 
            }
            report.Vehicle = vehicle;
            _unitOfWork.Report.Add(report);
            _unitOfWork.Save();
            return "Очтёт успешно создан";
        }

        public string Delete(Report report)
        {
            foreach (Message obj in _unitOfWork.Message.GetAll().Where(u => u.ReportId == report.Id))
            {
                _unitOfWork.Message.Delete(obj);
            }

            _unitOfWork.Report.Delete(report);
            _unitOfWork.Save();
            return "Водитель успешно удалён"; 
        }

        public Report? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public string Update(Report report)
        {
            throw new NotImplementedException();
        }

        public ReportVM CreateVM(Report report) 
        { 
            throw new NotImplementedException();
        }
    }
}
