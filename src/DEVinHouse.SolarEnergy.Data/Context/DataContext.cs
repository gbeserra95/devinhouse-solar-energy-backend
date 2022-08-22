using System.Reflection;
using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Data.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Mapping the database
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Import the mappings set that was configured in the Mappings folder
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Generation> Generations { get; set; }
    }
}