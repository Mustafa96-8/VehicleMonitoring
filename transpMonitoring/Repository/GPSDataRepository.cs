using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class GPSDataRepository : Repository<GPSData>, IGPSDataRepository
    {

        private ApplicationDbContext _context;

        public GPSDataRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(GPSData gPSData)
        {
            _context.GPSData.Update(gPSData);
        }
    }
}
