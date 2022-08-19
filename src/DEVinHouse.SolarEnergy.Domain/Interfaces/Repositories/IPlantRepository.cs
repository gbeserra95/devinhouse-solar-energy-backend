using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories
{
    public interface IPlantRepository
    {
        Task CreatePlantAsync(Plant plant);
        Task<Plant?> GetPlantByIdAsync(int id);
        Task<PlantsResponse> GetPlantsByNameAsync(string name, string userId, int page);
        Task<PlantsResponse> GetPlantsByAddressAsync(string address, string userId, int page);
        Task<PlantsResponse> GetPlantsByBrandAsync(string brand, string userId, int page);
        Task<PlantsResponse> GetPlantsByModelAsync(string model, string userId, int page);
        Task<PlantsResponse> GetPlantsByActiveStatusAsync(bool activeStatus, string userId, int page);
        Task<PlantsResponse> GetPlantsAsync(string userId, int page);
        Task UpdatePlantAsync(Plant plant);
        Task DeletePlantAsync(Plant plant);
    }
}