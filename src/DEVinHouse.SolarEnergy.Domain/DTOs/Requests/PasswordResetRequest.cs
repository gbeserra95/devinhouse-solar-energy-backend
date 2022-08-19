using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class PasswordResetRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }
                
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} is must have between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must be equal.")]
        public string ConfirmedPassword { get; set; }
    }
}