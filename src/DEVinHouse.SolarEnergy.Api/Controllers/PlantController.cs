using System.Security.Claims;
using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
using DEVinHouse.SolarEnergy.Domain.Entities;
using DEVinHouse.SolarEnergy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEVinHouse.SolarEnergy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.GetPlant(userId, id);

            if(result == null)
                return NoContent();
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PlantsResponse>> GetAll(int page, string? filter, bool? activeStatus)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.GetPlants(userId, page, filter, activeStatus);

            if(result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<PlantResponse>> AddPlant(PlantRequest plantRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.AddPlant(userId, plantRequest);

            if(result.Success)
                return Ok(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<ActionResult<PlantResponse>> UpdatePlant(int plantId, PlantRequest plantRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.UpdatePlant(userId, plantId, plantRequest);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public async Task<ActionResult<PlantResponse>> DeletePlant(int plantId)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.DeletePlant(userId, plantId);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}