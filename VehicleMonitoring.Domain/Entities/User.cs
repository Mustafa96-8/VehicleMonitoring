using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class User : Entity
    {
        
        [MaxLength(30,ErrorMessage ="Max 30 symb")]
        public string Login { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string Salt { get; set; }
        
        public string Role { get; set; }


        [MaxLength(50)]
        [DisplayName("Имя")]
        public string? FirstName { get; set; }
        
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string? LastName { get; set; }
        // Vehicles One To Many 
        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
/*        public User(string login, string passwordhash, string salt, string role, string? firstName, string? lastName)
        {
            Login = login;
            Passwordhash = passwordhash;
            Salt = salt;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
        }*/
    }

    
}
