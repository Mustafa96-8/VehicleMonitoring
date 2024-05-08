using VehicleMonitoring.Domain.Entities;
using VehicleMonitoring.mvc.ViewModels;

namespace VehicleMonitoring.mvc.Services.IServices
{
    public interface IDriverService : IService<Driver>
    {
        DriverVM CreateVM(Driver driver, bool isModifyFromVehicle);
    }
}
