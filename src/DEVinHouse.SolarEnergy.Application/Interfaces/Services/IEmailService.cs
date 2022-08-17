using DEVinHouse.SolarEnergy.Application.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(string email);
        Task<EmailConfirmationResponse> ConfirmEmail(string userId, string token);
        Task<PasswordForgottenResponse> ForgotPassword(string email);
    }
}