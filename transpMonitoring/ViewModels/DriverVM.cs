using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class DriverVM
    {
        public Driver Driver { get; set; }
        [ValidateNever]
        public bool isModifyFromVehicle { get; set; } = false;
        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleList { get; set; }
    }
}
