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

        /// <summary>
        /// Gets plant by Id. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response>  
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.GetPlant(userId, id);

            if(result == null)
                return NoContent();
            
            return Ok(result);
        }

        /// <summary>
        /// Gets a list of plants. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet]
        public async Task<ActionResult<PlantsResponse>> GetAll(int page, string? filter, bool? activeStatus)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _plantService.GetPlants(userId, page, filter, activeStatus);

            if(result.Success)
                return Ok(result);

            return NotFound(result);
        }

        /// <summary>
        /// Creates a new plant. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
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

        /// <summary>
        /// Updates an existing plant. Must be authenticated.
        /// </summary>    
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
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
        
        /// <summary>
        /// Deletes an existing plant. Must be authenticated.
        /// </summary>  
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
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