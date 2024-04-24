using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationTime { get; private set; } = DateTime.Now;
        // Vehicle Many To One
        [Required]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        [Required]
        public Vehicle Vehicle { get; set; }
        // Message One To Many to Child
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    }
}
