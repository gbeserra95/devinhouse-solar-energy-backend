using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Application.DTOs.Requests
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}