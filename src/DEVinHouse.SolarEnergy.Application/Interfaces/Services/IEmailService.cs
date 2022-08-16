using DEVinHouse.SolarEnergy.Application.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(string email);
        Task<ConfirmEmailResponse> ConfirmEmail(string userId, string token);
    }
}