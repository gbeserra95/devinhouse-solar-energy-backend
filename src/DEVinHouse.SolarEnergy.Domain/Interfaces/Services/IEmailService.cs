using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(string email);
        Task<EmailConfirmationResponse> ConfirmEmail(string userId, string token);
        Task<PasswordForgottenResponse> ForgotPassword(string email);
        Task<PasswordResetResponse> ResetPassword(string userId, string token, string newPassword);
    }
}