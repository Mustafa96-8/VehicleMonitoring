using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class Vehicle : Entity
    {

        [MaxLength(50)]
        public string? Brand { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [MaxLength(10)]
        public string? StateRegisterNumber { get; set; }

        // User One To Many
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        // Vehicle Description One To Many
        public ICollection<VehicleDescription> Descriptions  { get; } = new List<VehicleDescription>();

        // Driver One To One
        public int? DriverId { get; set; } 
        [ForeignKey("DriverId")]
        public Driver? Driver { get; }

        // GPS Data One To One
        public int? GPSDataId { get; set; }
        [ForeignKey("GPSDataId")]
        public GPSData? GPSData { get;}

    }
}
