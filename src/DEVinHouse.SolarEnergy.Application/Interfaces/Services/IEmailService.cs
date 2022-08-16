namespace DEVinHouse.SolarEnergy.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(string email);
    }
}