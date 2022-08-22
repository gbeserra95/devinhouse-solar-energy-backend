using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class UserUpdateRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}