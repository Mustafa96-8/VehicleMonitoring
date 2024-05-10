using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class VehicleVM
    {
        public Vehicle Vehicle { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UserList { get; set; }
        [ValidateNever]

        public Driver Driver { get; set; }
    }
}
