using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Data.Repositories
{
  public class PlantRepository : IPlantRepository
  {
    private readonly IBaseRepository<Plant> _baseRepository;
    private readonly DataContext _dataContext;

    public PlantRepository(IBaseRepository<Plant> baseRepository, DataContext dataContext)
    {
        _baseRepository = baseRepository;
        _dataContext = dataContext;        
    }

    public async Task<int> CreateAsync(Plant entity)
    {
      return await _baseRepository.CreateAsync(entity);
    }

    public async Task DeleteAsync(Plant entity)
    {
      await _baseRepository.DeleteAsync(entity);
    }

    public async Task<Plant?> FindByNameAsync(string name)
    {
      return await _dataContext.Plants.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Plant?> GetByIdAsync(int id)
    {
      return await _baseRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Plant>> GetPlantsAsync()
    {
      return await _baseRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Plant entity)
    {
        await _baseRepository.UpdateAsync(entity);
    }
  }
}