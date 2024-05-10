using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class GPSDataVM
    {
        public GPSData GPSData { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleList { get; set; }
    }
}
