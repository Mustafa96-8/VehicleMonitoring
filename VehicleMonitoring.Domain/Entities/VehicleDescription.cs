using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class VehicleDescription : Entity
    {

        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Content { get; set; }
        // Vehicle Many To One
        [Required]
        public int VehicleId { get; set; }
        [Required]
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; }

    }
}
