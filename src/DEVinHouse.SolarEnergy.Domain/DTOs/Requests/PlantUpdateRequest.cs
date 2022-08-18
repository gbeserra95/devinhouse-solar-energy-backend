using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class PlantUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}