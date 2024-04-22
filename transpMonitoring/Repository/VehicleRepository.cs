using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class VehicleRepository:Repository<Vehicle>,IVehicleRepository
    {
        private ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
        }
    }
}
