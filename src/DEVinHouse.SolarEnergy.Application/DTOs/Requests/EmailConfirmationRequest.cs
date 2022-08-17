using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Application.DTOs.Requests
{
    public class EmailConfirmationRequest
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string token {get; set; }
    }
}