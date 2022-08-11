using System.Reflection;
using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Mapping the database
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Import the mappings set
        }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Generation> Generations { get; set; }
    }
}