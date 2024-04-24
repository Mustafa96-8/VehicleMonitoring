using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class GPSData
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = "GPS Sensor";   
        // Vehicle One To One
        public int VehicleId { get; set; }

    }
}
