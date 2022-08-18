using DEVinHouse.SolarEnergy.Domain.DTOs.Requests;
using DEVinHouse.SolarEnergy.Domain.DTOs.Responses;
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

        [HttpPost("add-plant")]
        public async Task<ActionResult<PlantResponse>> AddPlant(PlantRequest plantRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = await _plantService.AddPlant(plantRequest);

            if(result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}