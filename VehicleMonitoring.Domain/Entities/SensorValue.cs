using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class SensorValue : Entity
    {

        [DisplayName("Значение показания датчика")]
        [Required]
        public double Value { get; set; }
        [DisplayName("Время считывания показания")]
        public DateTime CreationTime { get; private set; } = DateTime.UtcNow;

        // Sensor Many To One
        [DisplayName("Номер датчика")]
        [Required]
        public int SensorId{ get; set; }
        [Required]
        [ForeignKey("SensorId")]
        [ValidateNever]
        public Sensor Sensor { get; set; }


    }
}
