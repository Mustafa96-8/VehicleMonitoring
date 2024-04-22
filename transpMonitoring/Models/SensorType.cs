using System.ComponentModel.DataAnnotations;

namespace vehicleMonitoring.Models
{
    public class SensorType
    {
        [Required]

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
