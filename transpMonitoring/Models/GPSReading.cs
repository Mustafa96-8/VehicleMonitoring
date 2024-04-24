using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class GPSReading
    {
        [Key]
        public int Id{ get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double altitude { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }

        [Required]
        public DateTime CreationTime { get; private set; } = DateTime.Now;

        // GPSData Many To One to Parent
        [Required]
        public int GPSDataId{ get; set; }
        [ForeignKey("GPSDataId")]
        public GPSData GPSData { get; set; }
    }
}
