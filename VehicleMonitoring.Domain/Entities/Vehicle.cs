using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VehicleMonitoring.Domain.Entities
{
    public class Vehicle : Entity
    {

        [MaxLength(50)]
        [DisplayName("Бренд")]
        public string? Brand { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Модель")]
        public string Model { get; set; }
        [MaxLength(10)]
        [DisplayName("Регистрационный знак ТС")]
        public string? StateRegisterNumber { get; set; }

        // User One To Many
        [Required]
        [DisplayName("Номер прикреплённого пользователя")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; } = null!;
        
        // Vehicle Description One To Many
        public ICollection<VehicleDescription> Descriptions  { get; set; } = new List<VehicleDescription>();

        // Driver One To One
        public int? DriverId { get; set; } 
        [ForeignKey("DriverId")]

        public Driver? Driver { get; set; }

        // GPS Data One To One
        public int? GPSDataId { get; set; }
        [ForeignKey("GPSDataId")]
        public GPSData? GPSData { get; set; }

    }
}
