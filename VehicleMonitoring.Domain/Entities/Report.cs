using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMonitoring.Domain.Entities
{
    public class Report : Entity
    {


        [Required]
        public DateTime CreationTime { get; private set; } = DateTime.Now;
        // Vehicle Many To One
        [Required]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        [Required]
        [ValidateNever]
        public Vehicle Vehicle { get; set; }
        // Message One To Many to Child
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();

    }
}
