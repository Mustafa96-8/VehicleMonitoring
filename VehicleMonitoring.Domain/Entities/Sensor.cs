using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class Sensor : Entity
    {

        [MaxLength(50)]
        [DisplayName("Название датчика")]
        public string Name { get; set; }
        
        public double? ParametrUpper{ get; set; }
        
        public double? ParametrLower { get; set; }
        [DisplayName("Номер техники")]
        // Vehicle Many To One
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        [ValidateNever]
        public Vehicle Vehicle { get; set; }
        // SensorType Many To One 
        [Required]
        public int SensorTypeId { get; set; }
        [ForeignKey("SensorTypeId")]
        [ValidateNever]
        public SensorType SensorType { get; set; }

    }
}
