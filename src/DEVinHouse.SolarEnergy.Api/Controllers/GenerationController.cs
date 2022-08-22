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
    public class GenerationController : ControllerBase
    {
        private readonly IGenerationService _generationService;

        public GenerationController(IGenerationService generationService)
        {
            _generationService = generationService;
        }

        /// <summary>
        /// Gets a plant by Id. Must be authenticated.
        /// </summary>  
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Generation>> GetGeneration(int id)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _generationService.GetGeneration(userId, id);

            if(result is null)
                return NoContent();
            
            return Ok(result);
        }

        /// <summary>
        /// Gets a list of generations. Must be authenticated.
        /// </summary>  
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpGet]
        public async Task<ActionResult<GenerationsResponse>> GetAll(int? plantId, int page, DateTime? initialDate, DateTime? finalDate)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _generationService.GetGenerations(userId, plantId, page, initialDate, finalDate);

            if(result.Success)
                return Ok(result);

            return NotFound(result);
        }

        /// <summary>
        /// Creates a new generation. A plant only accepts one generation for month. Must be authenticated.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpPost]
        public async Task<ActionResult<GenerationResponse>> AddGeneration(GenerationRequest generationRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _generationService.AddGeneration(userId, generationRequest);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Updates an existing generation. Must be authenticated.
        /// </summary>  
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpPut]
        public async Task<ActionResult<GenerationResponse>> UpdateGeneration(int generationId, GenerationRequest generationRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _generationService.UpdateGeneration(userId, generationId, generationRequest);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Deletes an existing generation. Must be authenticated.
        /// </summary>  
        /// <response code="200">Success.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal Server Error.</response> 
        [HttpDelete]
        public async Task<ActionResult<GenerationResponse>> DeletePlant(int generationId)
        {
            var userId = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _generationService.DeleteGeneration(userId, generationId);

            if(result.Success)
                return Ok(result);
            else if(!result.Success)
                return BadRequest(result);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}