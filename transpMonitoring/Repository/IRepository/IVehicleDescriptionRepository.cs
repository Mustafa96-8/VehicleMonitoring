using vehicleMonitoring.Models;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IVehicleDescriptionRepository:IRepository<VehicleDescription>
    {
        void Update(VehicleDescription vehicleDescription);
    }
}
