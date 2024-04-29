using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class User(string login, string password) : Entity
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;

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
