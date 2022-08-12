using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories
{
    public interface IGenerationRepository
    {
        Task<int> CreateAsync(Generation entity);
        Task<Generation?> GetByIdAsync(int id);
        Task<Generation?> FindByDate(DateTime date);
        Task UpdateAsync(Generation entity);
        Task DeleteAsync(Generation entity);
        Task <IEnumerable<Generation>?> GetGenerationsAsync(int userId);
        Task <IEnumerable<Generation>?> GetByPlantAsync(int plantId);
    }
}