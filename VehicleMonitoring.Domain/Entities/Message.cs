using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class Message : Entity
    {

        [MaxLength(50)]
        public string Content { get; set; }
        [Required]                            
        public int Grade { get; set; }

        // Report Many To One to Parent
        [Required]
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        [ValidateNever]
        public Report Report { get; set; }
    }
}
