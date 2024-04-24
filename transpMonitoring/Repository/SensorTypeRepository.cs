using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
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
