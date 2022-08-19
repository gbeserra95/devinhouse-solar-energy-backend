using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories
{
    public interface IPlantRepository
    {
        Task CreatePlantAsync(Plant plant);
        Task<Plant?> GetPlantByIdAsync(int id);
        Task<PlantsResponse> GetPlantsAsync(string userId, int page, string? filter, bool? activeStatus);
        Task UpdatePlantAsync(Plant plant);
        Task DeletePlantAsync(Plant plant);
    }
}