using System.ComponentModel.DataAnnotations;

namespace transpMonitoring.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int SensorTypeId { get; set; }
    }
}
