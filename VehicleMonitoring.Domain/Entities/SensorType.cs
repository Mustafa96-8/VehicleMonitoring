using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleMonitoring.Domain.Entities
{
    public class SensorType : Entity
    {

        [DisplayName("Название типа датчиков")]
        [Required]
        public string Name { get; set; }

    }
}
