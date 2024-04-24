using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class SensorValueRepository:Repository<SensorValue>,ISensorValueRepository
    {
        private readonly ApplicationDbContext _context;

        public SensorValueRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(SensorValue sensorValue)
        {
            _context.SensorValues.Update(sensorValue);
        }
    }
}
