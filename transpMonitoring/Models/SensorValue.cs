using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace vehicleMonitoring.Models
{
    public class SensorValue
    {
        public int Id{ get; set; }
        [Required]
        public double Value { get; set; }

        [Required]
        public int SensorId{ get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}
