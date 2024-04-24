using Microsoft.EntityFrameworkCore;
using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
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
