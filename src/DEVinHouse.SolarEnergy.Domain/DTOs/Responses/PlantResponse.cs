namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class PlantResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public PlantResponse() => Errors = new List<string>();
        public PlantResponse(bool success = true) : this() => Success = success;

        public PlantResponse(bool success, string? message) : this()
        {
            Success = success;
            Message = message;
        }

        public void AddError(string error) => Errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}