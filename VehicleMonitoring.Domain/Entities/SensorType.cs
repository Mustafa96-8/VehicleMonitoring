using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class SensorType : Entity
    {

        [Required]
        public string Name { get; set; }

    }
}
