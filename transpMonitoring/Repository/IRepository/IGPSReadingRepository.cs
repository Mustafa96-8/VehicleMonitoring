using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IGPSReadingRepository:IRepository<GPSReading>
    {
        void Update(GPSReading gpsReading);
    }
}
