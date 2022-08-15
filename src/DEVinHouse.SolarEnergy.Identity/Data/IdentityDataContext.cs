using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Identity.Data
{
    public class IdentityDataContext : IdentityDbContext<User>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) {}
    }
}