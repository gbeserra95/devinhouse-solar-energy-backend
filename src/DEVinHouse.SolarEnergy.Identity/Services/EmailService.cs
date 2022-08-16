using DEVinHouse.SolarEnergy.Application.DTOs.Responses;
using DEVinHouse.SolarEnergy.Application.Interfaces.Services;
using DEVinHouse.SolarEnergy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

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
        var user = await _userManager.FindByEmailAsync(email.ToLower());
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

    public async Task<ConfirmEmailResponse> ConfirmEmail(string userId, string token)
    {
      var user = await _userManager.FindByIdAsync(userId);      
      var result = await _userManager.ConfirmEmailAsync(user, token);

			var confirmEmailResponse = new ConfirmEmailResponse(result.Succeeded);

      if(result.Succeeded)
        await _userManager.SetLockoutEnabledAsync(user, false);

			if(!result.Succeeded && result.Errors.Count() > 0)
				confirmEmailResponse.AddErrors(result.Errors.Select(err => err.Description));

			return confirmEmailResponse;
    }
  }
}