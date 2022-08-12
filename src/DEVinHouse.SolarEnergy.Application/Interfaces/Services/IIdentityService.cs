using DEVinHouse.SolarEnergy.Application.DTOs.Requests;
using DEVinHouse.SolarEnergy.Application.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
        Task<UserLoginResponse> Login(UserLoginRequest userLogin );
    }
}