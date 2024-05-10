using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Repository.IRepository
{
    public interface IVehicleDescriptionRepository:IRepository<VehicleDescription>
    {
        void Update(VehicleDescription vehicleDescription);
    }
}
