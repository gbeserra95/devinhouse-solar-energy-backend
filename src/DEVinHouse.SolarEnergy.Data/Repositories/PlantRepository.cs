using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
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

    public async Task<PlantsResponse> GetPlantsByNameAsync(string name, string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId && p.Name.Contains(name))
        .OrderBy(p => p.Name)
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task<PlantsResponse> GetPlantsByAddressAsync(string address, string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId && p.Address.Contains(address))
        .OrderBy(p => p.Name)
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task<PlantsResponse> GetPlantsByBrandAsync(string brand, string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId && p.Brand.Contains(brand))
        .OrderBy(p => p.Name)
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task<PlantsResponse> GetPlantsByModelAsync(string model, string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId && p.Model.Contains(model))
        .OrderBy(p => p.Name)
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task<PlantsResponse> GetPlantsByActiveStatusAsync(bool activeStatus, string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId && p.Active == activeStatus)
        .OrderBy(p => p.Name)
        .ToPaginatedRestAsync(page, limit);

      return new PlantsResponse(plants);
    }

    public async Task<PlantsResponse> GetPlantsAsync(string userId, int page)
    {
      if(page == 0)
        page = 1;
      
      var plants = await _dataContext.Plants
        .Where(p => p.UserId == userId)
        .OrderBy(p => p.Name)
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