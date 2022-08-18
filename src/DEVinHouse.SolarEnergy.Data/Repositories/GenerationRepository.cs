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

    public Task<int> CreateAsync(Generation entity)
    {
      throw new NotImplementedException();
    }

    public Task DeleteAsync(Generation entity)
    {
      throw new NotImplementedException();
    }

    public Task<Generation?> FindByDate(DateTime date)
    {
      throw new NotImplementedException();
    }

    public Task<Generation?> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Generation>?> GetByPlantAsync(int plantId)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Generation>?> GetGenerationsAsync(int userId)
    {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(Generation entity)
    {
      throw new NotImplementedException();
    }
  }
}