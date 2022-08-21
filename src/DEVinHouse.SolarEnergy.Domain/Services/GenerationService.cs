using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Repositories;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;

namespace DEVinHouse.SolarEnergy.Domain.Services
{
  public class GenerationService : IGenerationService
  {
    private readonly IGenerationRepository _generationRepository;
    private readonly IPlantRepository _plantRepository;

    public GenerationService(IGenerationRepository generationRepository, IPlantRepository plantRepository)
    {
		_generationRepository = generationRepository;
      	_plantRepository = plantRepository;
    }

    public async Task<GenerationResponse> AddGeneration(string userId, GenerationRequest generationRequest)
    {
      	var generationResponse = new GenerationResponse(true);

      	try 
      	{
			var generationBelongsToUser = await CheckIfPlantBelongsToUser(userId, generationRequest.PlantId);

			if (!generationBelongsToUser)
			{
				generationResponse.Success = false;
				generationResponse.Message = "Couldn't find any generation related to this user.";

				return generationResponse;
			}

        	var generation = new Generation(
				userId,
          		generationRequest.Date,
          		generationRequest.MonthlyConsumption,
          		generationRequest.PlantId
        	);

        	await _generationRepository.CreateGenerationAsync(generation);

			generationResponse.Message = "Generation created successfully.";
        	return generationResponse;
     	} 
      	catch(Exception e)
      	{
			generationResponse.Success = false;
          	generationResponse.Message = "Couldn't create generation.";

			generationResponse.AddError(e.Message);

			return generationResponse;
		}
    }

	public async Task<Generation?> GetGeneration(string userId, int generationId)
    {
		Generation? generation = await _generationRepository.GetGenerationByIdAsync(generationId);

		if(generation is null)
			return generation;

		var generationBelongsToUser = await CheckIfPlantBelongsToUser(userId, generation.PlantId);

		if (!generationBelongsToUser)
			return null;

		return generation;
    }

	public async Task<GenerationsResponse> GetGenerations(string userId, int plantId, int page, DateTime? initialDate, DateTime? finalDate)
    {
		var generationsResponse = await _generationRepository.GetGenerationsAsync(userId, plantId, page, initialDate, finalDate);
            
		if(generationsResponse.Generations.Count() == 0)
		{
			generationsResponse.Success = false;
			generationsResponse.Message = "Couldn't find any Generation that matches the query.";
		}

		return generationsResponse;
    }

	public async Task<GenerationResponse> UpdateGeneration(string userId, int generationId, GenerationRequest generationRequest)
    {
      	var generationResponse = new GenerationResponse(true);

		try 
		{
			Generation? generation = await _generationRepository.GetGenerationByIdAsync(generationId);

			if(generation is null)
			{
				generationResponse.Success = false;
				generationResponse.Message = "Couldn't find any generation.";

				return generationResponse;
			}

			var generationBelongsToUser = await CheckIfPlantBelongsToUser(userId, generation.PlantId);

			if (!generationBelongsToUser)
			{
				generationResponse.Success = false;
				generationResponse.Message = "Couldn't find any generation related to this user.";

				return generationResponse;
			}

			generation.UpdateGeneration(
				generationRequest.Date,
				generationRequest.MonthlyConsumption
			);

			await _generationRepository.UpdateGenerationAsync(generation);
			generationResponse.Message = "Generation updated successfully.";

			return generationResponse;
		} 
		catch(Exception e)
		{
			generationResponse.Success = false;
			generationResponse.Message = "Couldn't update generation.";

			generationResponse.AddError(e.Message);

			return generationResponse;
		}
    }

    public async Task<GenerationResponse> DeleteGeneration(string userId, int generationId)
    {
		var generationResponse = new GenerationResponse(true);

		try 
		{
			Generation? generation = await _generationRepository.GetGenerationByIdAsync(generationId);

			if(generation is null)
			{
				generationResponse.Success = false;
				generationResponse.Message = "Couldn't find any generation.";

				return generationResponse;
			}

			var generationBelongsToUser = await CheckIfPlantBelongsToUser(userId, generation.PlantId);

			if (!generationBelongsToUser)
			{
				generationResponse.Success = false;
				generationResponse.Message = "Couldn't find any generation related to this user.";

				return generationResponse;
			}

			await _generationRepository.DeleteGenerationAsync(generation);
			generationResponse.Message = "Generation deleted successfully.";

			return generationResponse;
		} 
		catch(Exception e)
		{
			generationResponse.Success = false;
			generationResponse.Message = "Couldn't update generation.";

			generationResponse.AddError(e.Message);

			return generationResponse;
		}
    }

	private async Task<bool> CheckIfPlantBelongsToUser(string userId, int plantId)
	{
		Plant? plant = await _plantRepository.GetPlantByIdAsync(plantId);

		if (plant is null || plant.UserId != userId)
			return false;

		return true;
	}
  }
}