namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class PlantResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public PlantResponse(bool success = true) => Success = success;

        public PlantResponse(bool success, string? message) : this()
        {
            Success = success;
            Message = message;
        }
    }
}