using vehicleMonitoring.Data;
using vehicleMonitoring.Models;
using vehicleMonitoring.Repository.IRepository;

namespace vehicleMonitoring.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        private ApplicationDbContext _context;

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
