using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IVehicleRepository:IRepository<Vehicle>
    {
        void Update(Vehicle vehicle);
    }
}
