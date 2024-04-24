﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleMonitoring.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [Required,MaxLength(50)]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [DisplayName("Отчество")]
        public string? MiddleName { get; set; }
        //Info about Driver  should be updated to more column
        [MaxLength(255)]
        [DisplayName("Общая информация")]
        public string? Information { get; set; }
        // Vehicle One To One
        public int VehicleId {  get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }
}