using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IReportRepository : IRepository<Report>
    {
        void Update(Report report);
    }
}
