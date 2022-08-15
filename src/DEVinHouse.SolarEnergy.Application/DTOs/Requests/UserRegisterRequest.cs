using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Application.DTOs.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} is must have between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords must be equal.")]
        public string ConfirmedPassword { get; set; }
    }
}