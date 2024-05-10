using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class SensorTypeRepository:Repository<SensorType>,ISensorTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public SensorTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(SensorType sensorType)
        {
            _context.SensorTypes.Update(sensorType);
        }
    }
}
