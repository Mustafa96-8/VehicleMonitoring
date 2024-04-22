using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace vehicleMonitoring.Models
{
    public class GPSReading
    {     
        public int Id{ get; set; }
        [AllowNull]
        public double latitude { get; set; }
        [AllowNull] 
        public double longitude { get; set; }
        [AllowNull] 
        public double altitude { get; set; }
        [AllowNull] 
        public double heading { get; set; }
        [AllowNull] 
        public double speed { get; set; }


        [Required,NotNull]
        public int GPSDataId{ get; set; }
        [Required]
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
