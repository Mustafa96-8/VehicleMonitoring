using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class GPSDataRepository : Repository<GPSData>, IGPSDataRepository
    {

        private readonly ApplicationDbContext _context;

        public GPSDataRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(GPSData GPSData)
        {
            _context.GPSData.Update(GPSData);
        }
    }
}
