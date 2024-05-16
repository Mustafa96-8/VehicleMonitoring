using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
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
            foreach (var obj in _unitOfWork.Message.GetAll().Where(u => u.ReportId == report.Id).ToList())
            {
                _unitOfWork.Message.Delete(obj);
            }

            _unitOfWork.Report.Delete(report);
            _unitOfWork.Save();
            return "Очтёт успешно удалён"; 
        }

        public Report? Get(int id)
        {
            Report? report = _unitOfWork.Report.Get(u => u.Id == id);

            report.Messages = _unitOfWork.Message.GetAll().Where(u => u.ReportId == report.Id).ToList();

            if (report.VehicleId != 0)
            {
                report.Vehicle = _unitOfWork.Vehicle.Get(u => u.Id == report.VehicleId);
            }
            return report;
        }

        public IEnumerable<Report> GetAll()
        {
            return _unitOfWork.Report.GetAll().ToList();
        }

        public string Update(Report report)
        {
            if (report.VehicleId == 0)
            { return "Указанное ТС не найдено"; }
            Vehicle? vehicle = _unitOfWork.Vehicle.Get(u => u.Id == report.VehicleId);
            if (vehicle == null) { return "Указанное ТС не найдено"; }
            _unitOfWork.Report.Update(report);
            _unitOfWork.Save();
            return "Отчёт изменён";
        }

        public ReportVM CreateVM(Report report) 
        {
            ReportVM reportVM = new ReportVM
            {
                Report = report,
                VehicleList = _unitOfWork.Vehicle.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Model + " | " + u.StateRegisterNumber,
                    Value = u.Id.ToString(),
                }),
            };
            return reportVM;
        }
    }
}
