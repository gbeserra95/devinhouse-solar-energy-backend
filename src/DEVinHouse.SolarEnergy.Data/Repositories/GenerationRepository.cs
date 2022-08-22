using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;

namespace DEVinHouse.SolarEnergy.Data.Repositories
{
  public class GenerationRepository : IGenerationRepository
  {
    private readonly IBaseRepository<Generation> _baseRepository;
    private readonly DataContext _dataContext;
    private const int limit = 20;

    public GenerationRepository(IBaseRepository<Generation> baseRepository, DataContext dataContext)
    {
        _baseRepository = baseRepository;
        _dataContext = dataContext;
    }

    public async Task CreateGenerationAsync(Generation generation)
    {
        await _baseRepository.CreateAsync(generation);
    }

    public async Task<Generation?> GetGenerationByIdAsync(int id)
    {
        return await _baseRepository.GetByIdAsync(id);
    }

    public async Task<GenerationsResponse> GetGenerationsAsync(string userId, int? plantId, int page, DateTime? initialDate, DateTime? finalDate)
    {
        if(page == 0)
            page = 1;

        var generations = await _dataContext.Generations
            .Where(g => g.UserId == userId)
            .Where(g => plantId == null || g.PlantId == plantId)
            .Where(g => initialDate == null || g.Date >= initialDate)
            .Where(g => finalDate == null || g.Date <= finalDate)
            .ToPaginatedRestAsync(page, limit);

        return new GenerationsResponse(generations);
    }

    public async Task UpdateGenerationAsync(Generation generation)
    {
        await _baseRepository.UpdateAsync(generation);
    }

    public async Task DeleteGenerationAsync(Generation generation)
    {
        await _baseRepository.DeleteAsync(generation);
    }
  }
}