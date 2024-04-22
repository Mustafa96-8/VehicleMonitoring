using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface ISensorTypeRepository:IRepository<SensorType>
    {
        void Update(SensorType sensorType);
    }
}
