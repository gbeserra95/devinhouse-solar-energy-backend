using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories
{
    public interface IGenerationRepository
    {
        Task CreateGenerationAsync(Generation generation);
        Task<Generation?> GetGenerationByIdAsync(int id);
        Task<GenerationsResponse> GetGenerationsAsync(string userId, int? plantId, int page, DateTime? initialDate, DateTime? finalDate);
        Task UpdateGenerationAsync(Generation generation);
        Task DeleteGenerationAsync(Generation generation);
    }
}