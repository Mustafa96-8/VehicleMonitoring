using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface ISensorValueRepository:IRepository<SensorValue>
    {
        void Update(SensorValue sensorValue);
    }
}
