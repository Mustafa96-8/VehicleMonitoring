using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class User(string login, string password,string role,string? firstName,string? lastName) : Entity
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;
        public string Role { get; set; } = role;

        
        [MaxLength(50)]
        [DisplayName("Имя")]
        public string? FirstName { get; set; } = firstName;
        
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string? LastName { get; set; } = lastName;
        // Vehicles One To Many 
        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
    }

    
}
