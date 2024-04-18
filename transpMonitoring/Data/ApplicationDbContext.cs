

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace transpMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

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
