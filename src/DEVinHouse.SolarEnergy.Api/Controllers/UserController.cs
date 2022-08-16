using DEVinHouse.SolarEnergy.Application.DTOs.Requests;
using DEVinHouse.SolarEnergy.Application.DTOs.Responses;
using DEVinHouse.SolarEnergy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DEVinHouse.SolarEnergy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            if(result.Success){
                return Ok(result);
            }
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

        [HttpGet("validate")]
        public async Task<ActionResult<ConfirmEmailResponse>> ValidateEmail(string userId, string token)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _emailService.ConfirmEmail(userId, token);

            if(result.Success)
                return Ok(result);
            else if(result.Errors.Count > 0)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
  }
}