using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegister);
        Task<UserLoginResponse> Login(UserLoginRequest userLogin );
        Task<UserRegisterResponse> UpdateUser(string userId, UserUpdateRequest userUpdateRequest);
        Task<UserDeleteResponse> DeleteUser(string email, string userId);
    }
}