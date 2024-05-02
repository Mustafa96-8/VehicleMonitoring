using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IVehicleService
    {
        string DeleteVehicle(Vehicle vehicle);
        Vehicle GetVehicle(int id);
        IEnumerable<Vehicle> GetVehicles();
    }
}