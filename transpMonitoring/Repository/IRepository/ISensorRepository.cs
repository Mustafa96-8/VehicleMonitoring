using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface ISensorRepository:IRepository<Sensor>
    {
        void Update(Sensor sensor);
    }
}
