using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEVinHouse.SolarEnergy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public UserController(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest userRegister)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _identityService.RegisterUser(userRegister);

            if(result.Success)
                return Ok(result);
            else if(result.Errors.Count > 0)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userLogin)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _identityService.Login(userLogin);

            if(result.Success)
                return Ok(result);

            return Unauthorized(result);
        }

        [HttpPost("resend-email")]
        public async Task<ActionResult<EmailConfirmationResponse>> ResendEmail(EmailRequest emailRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            await _emailService.SendEmailConfirmation(emailRequest.Email);

            return Ok(new EmailConfirmationResponse());
        }

        [HttpPost("validate-email")]
        public async Task<ActionResult<EmailConfirmationResponse>> ValidateEmail(EmailConfirmationRequest emailConfirmationRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _emailService.ConfirmEmail(emailConfirmationRequest.UserId.ToString(), emailConfirmationRequest.Token);

            if(result.Success)
                return Ok(result);
            else if(result.Errors.Count > 0)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<PasswordForgottenResponse>> ForgotPassword(PasswordForgottenRequest forgotPasswordRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _emailService.ForgotPassword(forgotPasswordRequest.Email);

            if(result.Success)
                return Ok(result);
            else if(result.Error != null)
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<PasswordResetResponse>> ResetPassword(PasswordResetRequest passwordResetRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _emailService.ResetPassword(passwordResetRequest.UserId.ToString(), passwordResetRequest.Token, passwordResetRequest.Password);

            if(result.Success)
                return Ok(result);
            else if(result.Errors.Count > 0)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
  }
}