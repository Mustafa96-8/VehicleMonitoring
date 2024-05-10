using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IReportService : IService<Report>
    {
        ReportVM CreateVM(Report report);
    }
}
