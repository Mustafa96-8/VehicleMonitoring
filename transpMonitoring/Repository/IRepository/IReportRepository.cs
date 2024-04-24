using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IReportRepository : IRepository<Report>
    {
        void Update(Report report);
    }
}
