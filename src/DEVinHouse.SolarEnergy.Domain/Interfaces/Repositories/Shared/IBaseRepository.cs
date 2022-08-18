using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories.Shared
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}