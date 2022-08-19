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
            base.OnModelCreating(modelBuilder); // Import the mappings set
        }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Generation> Generations { get; set; }
    }
}