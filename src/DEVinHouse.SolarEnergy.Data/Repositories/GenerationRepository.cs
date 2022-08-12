using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;

namespace DEVinHouse.SolarEnergy.Data.Repositories
{
  public class GenerationRepository : IGenerationRepository
  {
    private readonly IBaseRepository<Generation> _baseRepository;
    private readonly DataContext _dataContext;

    public GenerationRepository(IBaseRepository<Generation> baseRepository, DataContext dataContext)
    {
        _baseRepository = baseRepository;
        _dataContext = dataContext;
    }
     
    public async Task<int> CreateAsync(Generation entity)
    {
      return await _baseRepository.CreateAsync(entity);
    }

    public async Task DeleteAsync(Generation entity)
    {
      await _baseRepository.DeleteAsync(entity);
    }

    public async Task<Generation?> FindByDate(DateTime date)
    {
      throw new NotImplementedException();
    }

    public async Task<Generation?> GetByIdAsync(int id)
    {
      return await _baseRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Generation>?> GetByPlantAsync(int plantId)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Generation>?> GetGenerationsAsync(int userId)
    {
      return await _baseRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Generation entity)
    {
      await _baseRepository.UpdateAsync(entity);
    }
  }
}