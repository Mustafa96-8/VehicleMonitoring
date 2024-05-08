using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class VehicleDescriptionVM
    {
        public VehicleDescription VehicleDescription { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> vehicleList { get; set; }
    }
}
