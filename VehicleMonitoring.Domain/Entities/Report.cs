using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class Report : Entity
    {


        [DisplayName("Время создания отчёта")]
        [Required]
        public DateTime CreationTime { get; private set; } = DateTime.UtcNow;
        // Vehicle Many To One
        [DisplayName("Номер Техники")]
        [Required]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        [Required]
        [ValidateNever]
        public Vehicle Vehicle { get; set; }
        [DisplayName("Список Сообщений")]
        // Message One To Many to Child
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    }
}
