using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DEVinHouse.SolarEnergy.Application.DTOs.Requests;
using DEVinHouse.SolarEnergy.Application.DTOs.Responses;
using DEVinHouse.SolarEnergy.Application.Interfaces.Services;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DEVinHouse.SolarEnergy.Identity.Services
{
  public class IdentityService : IIdentityService
  {
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
    {
			_signInManager = signInManager;
			_userManager = userManager;
			_jwtOptions = jwtOptions.Value;
    }

		public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister)
    {
			var identityUser = new User
			{
				FirstName = userRegister.FirstName,
				LastName = userRegister.LastName,
				UserName = userRegister.Email,
				Email = userRegister.Email,
				EmailConfirmed = true
			};

				var result = await _userManager.CreateAsync(identityUser, userRegister.Password);
				if(result.Succeeded)
						await _userManager.SetLockoutEnabledAsync(identityUser, false);

			var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
			if(!result.Succeeded && result.Errors.Count() > 0)
				userRegisterResponse.AddErrors(result.Errors.Select(err => err.Description));

			return userRegisterResponse;
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
    {
			var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
			if(result.Succeeded)
				return await GenerateToken(userLogin.Email);

			var userLoginResponse = new UserLoginResponse(result.Succeeded);
			if (!result.Succeeded)
			{
				if(result.IsLockedOut)
					userLoginResponse.AddError("This user was blocked.");
				else if (result.IsNotAllowed)
					userLoginResponse.AddError("This user has no permission to log in.");
				else if(result.RequiresTwoFactor)
					userLoginResponse.AddError("Requires 2-factor authentication.");
				else
					userLoginResponse.AddError("User or password incorrect.");
			}

			return userLoginResponse;
    }

		private async Task<UserLoginResponse> GenerateToken(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			var tokenClaims = await GetClaims(user);

			var notBefore = DateTime.Now;
			var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

			var jwt = new JwtSecurityToken
			(
				issuer: _jwtOptions.Issuer,
				audience: _jwtOptions.Audience,
				claims: tokenClaims,
				notBefore: notBefore,
				expires: expirationDate,
				signingCredentials: _jwtOptions.SigningCredentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new UserLoginResponse
			(
				success: true,
				token: token,
				expirationDate: expirationDate
			);
		}
		private async Task<IList<Claim>> GetClaims(User user)
			{
				var claims = await _userManager.GetClaimsAsync(user);
				
				claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
				claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
				claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
				claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
				claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

				return claims;
			}
  }
}