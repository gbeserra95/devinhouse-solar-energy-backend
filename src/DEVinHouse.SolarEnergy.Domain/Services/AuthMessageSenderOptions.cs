namespace DEVinHouse.SolarEnergy.Domain.Services
{
    public class AuthMessageSenderOptions
    {
        public string? SendGridName { get; set; }
        public string? SendGridEmail { get; set; }
        public string? ConfirmEmailTemplateId { get; set; }
        public string? ResetPasswordEmailTemplateId { get; set; }
        public string? SendGridKey { get; set; }
    }
}