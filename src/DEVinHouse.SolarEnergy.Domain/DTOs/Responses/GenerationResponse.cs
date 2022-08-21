namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class GenerationResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public GenerationResponse() => Errors = new List<string>();
        public GenerationResponse(bool success = true) : this() => Success = success;

        public GenerationResponse(bool success, string? message) : this()
        {
            Success = success;
            Message = message;
        }

        public void AddError(string error) => Errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}