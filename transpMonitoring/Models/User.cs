using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vehicleMonitoring.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        // Vehicles One To Many 
        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
    }
}
