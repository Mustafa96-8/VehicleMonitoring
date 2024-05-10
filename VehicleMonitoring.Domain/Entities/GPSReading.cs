using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class GPSReading : Entity
    {
        [DisplayName("Широта")]

        public double latitude { get; set; }
        [DisplayName("Долгота")]
        public double longitude { get; set; }
        [DisplayName("Высота")]
        public double altitude { get; set; }
        [DisplayName("Направление")]
        public double heading { get; set; }
        [DisplayName("Скорость")]
        public double speed { get; set; }

        [DisplayName("Дата Записи")]
        [Required]
        public DateTime CreationTime { get; private set; } = DateTime.UtcNow ;

        // GPSData Many To One to Parent
        [DisplayName("Номер Датчика GPS")]
        [Required]
        public int GPSDataId{ get; set; }
        [ForeignKey("GPSDataId")]
        [ValidateNever]
        public GPSData GPSData { get; set; }
    }
}
