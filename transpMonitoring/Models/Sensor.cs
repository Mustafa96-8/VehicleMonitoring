using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [DisplayName("Название датчика")]
        public string Name { get; set; }
        
        public double ParametrUpper{ get; set; }
        
        public double ParametrLower { get; set; }
        // Vehicle Many To One
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        // SensorType Many To One 
        [Required]
        public int SensorTypeId { get; set; }
        [ForeignKey("SensorTypeId")]
        public SensorType SensorType { get; set; }

    }
}
