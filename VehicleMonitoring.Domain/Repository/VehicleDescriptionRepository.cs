using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class VehicleDescriptionRepository:Repository<VehicleDescription>,IVehicleDescriptionRepository
    {
        private readonly ApplicationDbContext _context;

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
