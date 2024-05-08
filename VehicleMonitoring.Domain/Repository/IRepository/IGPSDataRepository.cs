using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IGPSDataRepository:IRepository<GPSData>
    {
        void Update(GPSData GPSData);
    }
}
