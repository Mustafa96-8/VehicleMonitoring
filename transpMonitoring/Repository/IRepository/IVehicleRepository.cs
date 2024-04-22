using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IVehicleRepository:IRepository<Vehicle>
    {
        void Update(Vehicle vehicle);
    }
}
