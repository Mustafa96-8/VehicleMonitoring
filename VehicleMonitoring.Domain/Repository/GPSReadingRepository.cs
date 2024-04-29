using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class GPSReadingRepository:Repository<GPSReading>,IGPSReadingRepository
    {
        private readonly ApplicationDbContext _context;

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
