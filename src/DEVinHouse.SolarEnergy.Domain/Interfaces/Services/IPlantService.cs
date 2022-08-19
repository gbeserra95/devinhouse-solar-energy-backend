using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IPlantService
    {
        Task<PlantsResponse> GetPlants(string userId, int page, string? filter, bool? activeStatus);
        Task<PlantResponse> AddPlant(string userId, PlantRequest plantRequest);
    }
}