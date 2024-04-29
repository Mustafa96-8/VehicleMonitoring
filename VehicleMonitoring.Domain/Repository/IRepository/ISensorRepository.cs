using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface ISensorRepository:IRepository<Sensor>
    {
        void Update(Sensor sensor);
    }
}
