namespace DEVinHouse.SolarEnergy.Application.DTOs.Responses
{
    public class PasswordForgottenResponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }

        public PasswordForgottenResponse(bool success = true)
        {
            Success = success;
        }
    }
}