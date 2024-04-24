using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class SensorValue
    {
        public int Id{ get; set; }
        [Required]
        public double Value { get; set; }
        public DateTime CreationTime { get; private set; } = DateTime.Now;

        // Sensor Many To One
        [Required]
        public int SensorId{ get; set; }
        [Required]
        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }


    }
}
