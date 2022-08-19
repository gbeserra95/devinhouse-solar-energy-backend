using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;
using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using DEVinHouse.SolarEnergy.Domain.Services;

namespace DEVinHouse.SolarEnergy.Identity.Services
{
  public class EmailService : IEmailService
  {
    private readonly AuthMessageSenderOptions _options;
    private readonly UserManager<User> _userManager;

    public EmailService(IOptions<AuthMessageSenderOptions> options, UserManager<User> userManager)
    {
        _options = options.Value;
        _userManager = userManager;
    }

    public async Task SendEmailConfirmation(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var client = new SendGridClient(_options.SendGridKey);
        var msg = new SendGridMessage();
        msg.SetFrom(_options.SendGridEmail, _options.SendGridName);
        msg.AddTo(user.Email, user.FirstName);
        msg.SetTemplateId(_options.ConfirmEmailTemplateId);
        msg.SetTemplateData(new
        {
            user_name = user.FirstName,
            user_email = user.Email,
            confirmation_url = $"https://localhost:7116/api/validate/?userId={user.Id}&token={token}"
        });

        await client.SendEmailAsync(msg);
    }

    public async Task<EmailConfirmationResponse> ConfirmEmail(string userId, string token)
    {
      var user = await _userManager.FindByIdAsync(userId);      
      var result = await _userManager.ConfirmEmailAsync(user, token);

			var confirmEmailResponse = new EmailConfirmationResponse(result.Succeeded);

      if(result.Succeeded)
        await _userManager.SetLockoutEnabledAsync(user, false);

			if(!result.Succeeded && result.Errors.Count() > 0)
				confirmEmailResponse.AddErrors(result.Errors.Select(err => err.Description));

			return confirmEmailResponse;
    }

    public async Task<PasswordForgottenResponse> ForgotPassword(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      var client = new SendGridClient(_options.SendGridKey);
      var msg = new SendGridMessage();
      msg.SetFrom(_options.SendGridEmail, _options.SendGridName);
      msg.AddTo(user.Email, user.FirstName);
      msg.SetTemplateId(_options.ResetPasswordEmailTemplateId);
      msg.SetTemplateData(new
      {
          user_name = user.FirstName,
          confirmation_url = $"https://localhost:7116/api/reset-password/?userId={user.Id}&token={token}"
      });

      var response = await client.SendEmailAsync(msg);
      var forgotPasswordResponse = new PasswordForgottenResponse(response.IsSuccessStatusCode);

      if(!response.IsSuccessStatusCode)
        forgotPasswordResponse.Error = "Email could NOT be sent";

      return forgotPasswordResponse;
    }

    public async Task<PasswordResetResponse> ResetPassword(string userId, string token, string newPassword)
    {
      var user = await _userManager.FindByIdAsync(userId);      
      var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

			var passwordResetResponse = new PasswordResetResponse(result.Succeeded);

			if(!result.Succeeded && result.Errors.Count() > 0)
				passwordResetResponse.AddErrors(result.Errors.Select(err => err.Description));

			return passwordResetResponse;
    }
  }
}