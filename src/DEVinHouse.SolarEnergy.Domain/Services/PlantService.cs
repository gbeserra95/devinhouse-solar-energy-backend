using Canducci.Pagination;
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

        public async Task<PlantResponse> AddPlant(string userId, PlantRequest plantRequest)
        {
            var plantResponse = new PlantResponse(true);

            try 
            {
                var plant = new Plant(
                    userId,
                    plantRequest.Name,
                    plantRequest.Address,
                    plantRequest.Brand,
                    plantRequest.Model,
                    plantRequest.Active
                );

                await _plantRepository.CreatePlantAsync(plant);

                plantResponse.Message = "Plant created successfully.";
                return plantResponse;
            } 
            catch(Exception e)
            {
                plantResponse.Success = false;
                plantResponse.Message = "Couldn't create plant.";

                plantResponse.AddError(e.Message);

                return plantResponse;
            }
        }

        public async Task<PlantsResponse> GetPlants(string userId, int page, string? filter, bool? activeStatus)
        {
            var plantsResponse = await _plantRepository.GetPlantsAsync(userId, page, filter, activeStatus);
            
            if(plantsResponse.Plants.Count() == 0)
            {
                plantsResponse.Success = false;
                plantsResponse.Message = "Couldn't find any Plant that matches the query.";
            }

            return plantsResponse;
        }
    }
}