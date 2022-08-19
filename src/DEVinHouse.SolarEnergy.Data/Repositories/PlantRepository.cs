using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;

namespace DEVinHouse.SolarEnergy.Data.Repositories
{
  public class PlantRepository : IPlantRepository
  {
    private readonly IBaseRepository<Plant> _baseRepository;
    private readonly DataContext _dataContext;
    private const int limit = 20;

    public PlantRepository(IBaseRepository<Plant> baseRepository, DataContext dataContext)
    {
        _baseRepository = baseRepository;
        _dataContext = dataContext;        
    }

    public async Task CreatePlantAsync(Plant plant)
    {
      await _baseRepository.CreateAsync(plant);
    }

    public async Task<Plant?> GetPlantByIdAsync(int id)
    {
      return await _baseRepository.GetByIdAsync(id);
    }

    public async Task<PlantsResponse> GetPlantsAsync(string userId, int page, string? filter, bool? activeStatus)
    {
      if(page == 0)
        page = 1;

      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId)
        .OrderByDescending(p => p.Id)
        .Where(p => activeStatus == null || p.Active == activeStatus)
        .Where(
          p => string.IsNullOrEmpty(filter)
          || p.Name.Contains(filter)
          || p.Address.Contains(filter)
          || p.Brand.Contains(filter)
          || p.Model.Contains(filter)
        )
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task UpdatePlantAsync(Plant plant)
    {
      await _baseRepository.UpdateAsync(plant);
    }

    public async Task DeletePlantAsync(Plant plant)
    {
      await _baseRepository.DeleteAsync(plant);
    }
  }
}