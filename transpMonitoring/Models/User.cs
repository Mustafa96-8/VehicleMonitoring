using System.ComponentModel.DataAnnotations;

namespace transpMonitoring.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<int> VehiclesId { get; set; } = [];
    }
}
