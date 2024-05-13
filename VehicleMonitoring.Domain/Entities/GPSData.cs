using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class GPSData: Entity
    {

        [MaxLength(50)]
        [DisplayName("Название")]
        [Required]
        public string Name { get; set; } = "GPS Sensor";   
        [DisplayName("Номер Техники")]
        // Vehicle One To One
        public int VehicleId { get; set; }

    }
}
