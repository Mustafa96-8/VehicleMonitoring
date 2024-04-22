

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vehicleMonitoring.Models;

namespace vehicleMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<GPSData> GPSData { get; set; }
        public DbSet<GPSReading> GPSReadings { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValue> SensorValues { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDescription> VehicleDescriptions{ get; set; }



        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Slovo o Polku Igoreve", Author = "Victor Luninin", Description = "For Child", Price = 250, CategoryId = 3, imageURL = "" },
                new Product { Id = 2, Title = "Roadside Picnic", Author = "Brothers Strugatski", Description = "Adventure on anomaly place \"Zona\"", Price = 350, CategoryId = 1, imageURL = "" },
                new Product { Id = 3, Title = "Metro", Author = "Dmitry Glukhovsky", Description = "Radioactive adventure Russian metro", Price = 300, CategoryId = 2, imageURL = "" }
                );
        }*/
    }
}
