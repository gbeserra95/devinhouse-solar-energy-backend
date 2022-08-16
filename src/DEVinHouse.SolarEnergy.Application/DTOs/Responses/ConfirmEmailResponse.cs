using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVinHouse.SolarEnergy.Application.DTOs.Responses
{
    public class ConfirmEmailResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public ConfirmEmailResponse() => Errors = new List<string>();
        public ConfirmEmailResponse(bool success = true) : this() => Success = success;
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
    }
}