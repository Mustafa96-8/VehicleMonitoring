using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IVehicleDescriptionService:IService<VehicleDescription>
    {
        List<VehicleDescription> GetByVehicleId(int vehicleId);

    }
}
