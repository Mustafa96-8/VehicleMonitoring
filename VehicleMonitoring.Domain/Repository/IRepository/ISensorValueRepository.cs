using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface ISensorValueRepository:IRepository<SensorValue>
    {
        void Update(SensorValue sensorValue);
    }
}
