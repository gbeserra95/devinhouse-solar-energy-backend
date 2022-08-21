using System.ComponentModel.DataAnnotations;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Requests
{
    public class GenerationRequest
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal MonthlyConsumption { get; set; }

        [Required]
        public int PlantId { get; set; }
    }
}