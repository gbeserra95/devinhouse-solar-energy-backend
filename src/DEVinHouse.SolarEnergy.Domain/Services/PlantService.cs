using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;

namespace DEVinHouse.SolarEnergy.Domain.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;

        public PlantService(IPlantRepository plantRepository)
        {
        _plantRepository = plantRepository;
        }

        public async Task<PlantResponse> AddPlant(PlantRequest plantRequest)
        {
            var plant = new Plant(
                plantRequest.UserId,
                plantRequest.Name,
                plantRequest.Address,
                plantRequest.Brand,
                plantRequest.Model,
                plantRequest.Active
            );

            return await _plantRepository.CreatePlantAsync(plant);
        }
    }
}