using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IPlantService
    {
        Task<Plant?> GetPlant(string userId, int plantId);
        Task<PlantsResponse> GetPlants(string userId, int page, string? filter, bool? activeStatus);
        Task<PlantResponse> AddPlant(string userId, PlantRequest plantRequest);
        Task<PlantResponse> UpdatePlant(string userId, int plantId, PlantRequest plantRequest);
        Task<PlantResponse> DeletePlant(string userId, int plantId);
    }
}