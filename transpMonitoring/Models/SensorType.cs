using System.ComponentModel.DataAnnotations;

namespace transpMonitoring.Models
{
    public class SensorType
    {
        [Required]

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
