

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.Domain.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<GPSData> GPSData { get; set; }
        public DbSet<GPSReading> GPSReadings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValue> SensorValues { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDescription> VehicleDescriptions{ get; set; }

        //public DbSet<Role> Roles { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorType>().HasData(
                new SensorType { Id = 1, Name = "Standart" },
                new SensorType { Id = 2, Name = "Fuel level" },
                new SensorType { Id = 3, Name = "Total Fuel level" },
                new SensorType { Id = 4, Name = "Velocity" },
                new SensorType { Id = 5, Name = "Acceleration sensor" },
                new SensorType { Id = 6, Name = "Lambda sensor" },
                new SensorType { Id = 7, Name = "Ignition" }
                );
        }
    }
}
