using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Content { get; set; }
        [Required]                            
        public int Grade { get; set; }

        // Report Many To One to Parent
        [Required]
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public Report Report { get; set; }
    }
}
