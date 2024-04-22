using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IGPSDataRepository:IRepository<GPSData>
    {
        void Update(GPSData gpsData);
    }
}
