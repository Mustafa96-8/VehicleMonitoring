﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace vehicleMonitoring.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [MaxLength(50)]
        [DisplayName("Название датчика")]
        public string Name { get; set; }
        [AllowNull]
        public double ParametrUpper{ get; set; }
        [AllowNull]
        public double ParametrLower { get; set; }
        [Required]
        public int SensorTypeId { get; set; }
    }
}
