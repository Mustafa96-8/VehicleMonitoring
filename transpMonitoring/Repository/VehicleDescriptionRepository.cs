using vehicleMonitoring.Models;
using vehicleMonitoring.Data;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class VehicleDescriptionRepository:Repository<VehicleDescription>,IVehicleDescriptionRepository
    {
        private ApplicationDbContext _context;

        public VehicleDescriptionRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(VehicleDescription vehicleDescription)
        {
            _context.VehicleDescriptions.Update(vehicleDescription);
        }
    }
}
