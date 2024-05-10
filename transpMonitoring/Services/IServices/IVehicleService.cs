using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IVehicleService : IService<Vehicle>
    {
        VehicleVM CreateVM(Vehicle vehicle);
        IEnumerable<VehicleVM> GetAllVM();
    }
}