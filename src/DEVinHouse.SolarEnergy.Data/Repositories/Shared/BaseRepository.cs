using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.Entities.Shared;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace DEVinHouse.SolarEnergy.Data.Repositories.Shared
{
  public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
  {
    public readonly DbSet<TEntity> _DbSet;
    public readonly DataContext _dataContext;
    public BaseRepository(DataContext dataContext)
    {
        _DbSet = dataContext.Set<TEntity>();
        _dataContext = dataContext;
    }

    public async Task<int> CreateAsync(TEntity entity)
    {
      var response = await _DbSet.AddAsync(entity);
      await _dataContext.SaveChangesAsync();

      return response.Entity.Id;
    }

    public async Task DeleteAsync(TEntity entity)
    {
      _DbSet.Remove(entity);
      await _dataContext.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
      return await _DbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
      return await _DbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
      _DbSet.Update(entity);
      await _dataContext.SaveChangesAsync();
    }
  }
}