using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Data.Context;
using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
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
    private const int limit = 20;

    public PlantRepository(IBaseRepository<Plant> baseRepository, DataContext dataContext)
    {
        _baseRepository = baseRepository;
        _dataContext = dataContext;        
    }

    public async Task<PlantResponse> CreatePlantAsync(Plant plant)
    {
      await _baseRepository.CreateAsync(plant);

      var plantResponse = new PlantResponse();
      plantResponse.Message = "Plant was successfully created.";

      return plantResponse;
    }

    public async Task<PlantResponse> GetPlantByIdAsync(int id)
    {
      var plant = await _baseRepository.GetByIdAsync(id);
      var plantResponse = new PlantResponse();

      if(plant == null)
      {
        plantResponse.Success = false;
        plantResponse.Message = "Plant could NOT be found.";

        return plantResponse;
      }

      plantResponse.Success = true;
      plantResponse.Plant = plant;

      return plantResponse;
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

    public async Task<PlantResponse> UpdatePlantAsync(PlantUpdateRequest plantUpdateRequest)
    {
      var plant = await _baseRepository.GetByIdAsync(plantUpdateRequest.Id);
      var plantResponse = new PlantResponse();

      if(plant == null)
      {
        plantResponse.Success = false;
        plantResponse.Message = "Plant could NOT be found.";

        return plantResponse;
      }

      plant.UpdatePlant(
        plantUpdateRequest.Name,
        plantUpdateRequest.Address,
        plantUpdateRequest.Brand,
        plantUpdateRequest.Model,
        plantUpdateRequest.Active
      );

      await _baseRepository.UpdateAsync(plant);

      plantResponse.Success = true;
      plantResponse.Message = "Plant was updated.";

      return plantResponse;
    }

    public async Task<PlantResponse> DeletePlantAsync(Plant plant)
    {
      await _baseRepository.DeleteAsync(plant);

      var plantResponse = new PlantResponse(true);
      plantResponse.Message = "Plant was successfully deleted.";

      return plantResponse;
    }
  }
}