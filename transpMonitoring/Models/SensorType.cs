using System.ComponentModel.DataAnnotations;

namespace vehicleMonitoring.Models
{
    public class SensorType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
