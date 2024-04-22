using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class GPSReadingRepository:Repository<GPSReading>,IGPSReadingRepository
    {
        private ApplicationDbContext _context;

        public GPSReadingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(GPSReading gPSReading)
        {
            _context.GPSReadings.Update(gPSReading);
        }
    }
}
