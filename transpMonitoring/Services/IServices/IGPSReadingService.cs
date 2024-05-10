using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IGPSReadingService : IService<GPSReading>
    {
        IEnumerable<GPSReading> GetFirstN(int? N = 10);
    }
}
