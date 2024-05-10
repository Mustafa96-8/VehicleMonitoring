using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IVehicleDescriptionService:IService<VehicleDescription>
    {
        VehicleDescriptionVM CreateVM(VehicleDescription vehicleDescription);
        List<VehicleDescription> GetByVehicleId(int vehicleId);

    }
}
