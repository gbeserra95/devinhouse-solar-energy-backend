using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class PlantResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Plant? Plant { get; set; }
        public PlantResponse(bool success = true) => Success = success;
    }
}