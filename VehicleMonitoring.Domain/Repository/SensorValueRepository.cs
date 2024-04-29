using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
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
