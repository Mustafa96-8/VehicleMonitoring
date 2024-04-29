using Microsoft.EntityFrameworkCore;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class ReportRepository:Repository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Report report)
        {
            _context.Reports.Update(report);
        }

    }
}
