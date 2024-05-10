using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class VehicleDescription : Entity
    {

        [Required, MaxLength(50)]
        [DisplayName("Название описания")]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        [DisplayName("Содержание")]
        public string Content { get; set; }
        // Vehicle Many To One
        [Required]
        [DisplayName("Номер техники")]
        public int VehicleId { get; set; }
        [Required]
        [ForeignKey("VehicleId")]
        [ValidateNever]
        public Vehicle Vehicle { get; set; }

    }
}
