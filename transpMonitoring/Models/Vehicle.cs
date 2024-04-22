using System.ComponentModel.DataAnnotations;

namespace vehicleMonitoring.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Brand { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [MaxLength(10)]
        public string? StateRegisterNumber { get; set; }
        
        [Required]
        public int UserId { get; set; }

        public ICollection<VehicleDescription> Descriptions { get; } = new List<VehicleDescription>();

        public int DriverId { get; set; }

    }
}
