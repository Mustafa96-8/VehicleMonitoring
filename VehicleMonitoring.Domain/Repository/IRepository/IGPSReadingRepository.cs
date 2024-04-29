using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IGPSReadingRepository:IRepository<GPSReading>
    {
        void Update(GPSReading gpsReading);
    }
}
