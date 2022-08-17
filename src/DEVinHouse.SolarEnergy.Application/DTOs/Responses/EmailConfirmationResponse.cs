using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinHouse.SolarEnergy.Application.DTOs.Responses
{
    public class EmailConfirmationResponse
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }
        public EmailConfirmationResponse() => Errors = new List<string>();
        public EmailConfirmationResponse(bool success = true) : this() => Success = success;
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}