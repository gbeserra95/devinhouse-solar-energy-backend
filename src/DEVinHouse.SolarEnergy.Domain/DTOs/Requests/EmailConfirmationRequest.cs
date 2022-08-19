using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class EmailConfirmationRequest
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string Token {get; set; }
    }
}