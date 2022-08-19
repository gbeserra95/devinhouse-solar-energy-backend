namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class PasswordResetResponse
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }
        public PasswordResetResponse() => Errors = new List<string>();
        public PasswordResetResponse(bool success = true) : this() => Success = success;
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}