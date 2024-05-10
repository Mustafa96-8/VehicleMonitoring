using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface ISensorTypeRepository:IRepository<SensorType>
    {
        void Update(SensorType sensorType);
    }
}
