using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface ISensorService :IService<Sensor>
    {
        public SensorVM CreateVM(Sensor sensor);
    }
}
