using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class DriverRepository : Repository<Driver>,IDriverRepository
    {
        private readonly ApplicationDbContext _context;

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
