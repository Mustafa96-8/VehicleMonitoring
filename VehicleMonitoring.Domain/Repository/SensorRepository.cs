using VehicleMonitoring.Domain.Data;
using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.Domain.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        private readonly ApplicationDbContext _context;

        public SensorRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(Sensor sensor)
        {
            _context.Sensors.Update(sensor);
        }
    }
}
