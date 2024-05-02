using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.Domain.Repository.IRepository;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface ISensorTypeService
    {
        IEnumerable<SensorType> GetSensorTypes();
        SensorType GetSensorType(int id);
        string DeleteSensorType(SensorType sensorType);
    }
}
