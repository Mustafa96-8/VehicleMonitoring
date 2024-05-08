using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class GPSData: Entity
    {

        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = "GPS Sensor";   
        // Vehicle One To One
        public int VehicleId { get; set; }

    }
}
