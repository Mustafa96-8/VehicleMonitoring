using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.ViewModels
{
    public class MessageVM
    {
        public Message Message { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ReportList { get; set; }
    }
}
