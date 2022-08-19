using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IPlantService
    {
        Task<PlantResponse> AddPlant(string userId, PlantRequest plantRequest);
    }
}