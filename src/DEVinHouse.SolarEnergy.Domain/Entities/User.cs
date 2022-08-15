using Microsoft.AspNetCore.Identity;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}