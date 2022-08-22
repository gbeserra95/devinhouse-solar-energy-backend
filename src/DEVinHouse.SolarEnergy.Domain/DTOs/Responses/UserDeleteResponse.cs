namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class UserDeleteResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public UserDeleteResponse(bool success = true) => Success = success;
    }
}