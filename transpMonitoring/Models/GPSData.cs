using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace vehicleMonitoring.Models
{
    public class GPSData
    {
        [Required]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = "GPS Sensor";
    }
}
