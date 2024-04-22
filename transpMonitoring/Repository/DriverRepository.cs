using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class DriverRepository : Repository<Driver>,IDriverRepository
    {
        private ApplicationDbContext _context;

        public DriverRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(Driver driver)
        {
            _context.Drivers.Update(driver);
        }

    }
}
