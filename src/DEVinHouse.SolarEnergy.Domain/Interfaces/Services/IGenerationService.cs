using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Services
{
    public interface IGenerationService
    {
        Task<Generation?> GetGeneration(string userId, int generationId);
        Task<GenerationsResponse> GetGenerations(string userId, int plantId, int page, DateTime? initialDate, DateTime? finalDate);
        Task<GenerationResponse> AddGeneration(string userId, GenerationRequest generationRequest);
        Task<GenerationResponse> UpdateGeneration(string userId, int generationId, GenerationRequest generationRequest);
        Task<GenerationResponse> DeleteGeneration(string userId, int generationId);  
    }
}