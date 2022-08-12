using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories
{
    public interface IPlantRepository
    {
        Task<int> CreateAsync(Plant entity);
        Task<Plant?> GetByIdAsync(int id);
        Task<Plant?> FindByNameAsync(string name);
        Task UpdateAsync(Plant entity);
        Task DeleteAsync(Plant entity);
        Task <IEnumerable<Plant>> GetPlantsAsync();
    }
}