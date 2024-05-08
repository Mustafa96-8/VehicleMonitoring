using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IGPSDataService : IService<GPSData>
    {
        GPSDataVM CreateVM(GPSData gPSData);
    }
}
