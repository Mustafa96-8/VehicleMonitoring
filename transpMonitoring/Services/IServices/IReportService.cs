using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.Extensions;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IReportService : IService<Report>
    {
        ReportHandler ReportHandler { get; }
        ReportVM CreateVM(Report report);
        IEnumerable<Report> GetByVehicleId(int vehicleId);
    }
}
