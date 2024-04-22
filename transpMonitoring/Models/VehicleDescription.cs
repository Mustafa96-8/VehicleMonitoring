using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class VehicleDescription
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Content { get; set; }
        [Required]
        public int VehicleId { get; set; }

    }
}
