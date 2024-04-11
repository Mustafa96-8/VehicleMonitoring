using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using transpMonitoring.Models;

namespace vehilceMonitoring.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required,MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? MiddleName { get; set; }
        //Info about Driver  should be updated to more column
        [AllowNull]
        [MaxLength(255)]
        public string? Information { get; set; }
        public int VehicleId {  get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
