using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class PasswordForgottenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}