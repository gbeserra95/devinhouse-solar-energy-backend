using System.Security.Claims;
using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>    
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

        /// <summary>
        /// Login.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response>  
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

        /// <summary>
        /// Resends email verification.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="500">Internal Server Error.</response>  
        [HttpPost("resend-email")]
        public async Task<ActionResult<EmailConfirmationResponse>> ResendEmail(EmailRequest emailRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            await _emailService.SendEmailConfirmation(emailRequest.Email);

            return Ok(new EmailConfirmationResponse());
        }

        /// <summary>
        /// Validates user with token received via email.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>  
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

        /// <summary>
        /// Sends password change request via email.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>  
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

        /// <summary>
        /// Resets password with token received via email.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>  
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

        /// <summary>
        /// Removes an existing. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response>  
        [Authorize]
        [HttpDelete("remove-user")]
        public async Task<ActionResult<UserDeleteResponse>> DeleteUser(string email)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _identityService.DeleteUser(email, userId);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Updates an existing user. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response>  
        [HttpPut("update-user")]
        public async Task<ActionResult<UserRegisterResponse>> UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _identityService.UpdateUser(userId, userUpdateRequest);

            if(result.Success)
                return Ok(result);
            else if(result.Errors.Count > 0)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
  }
}