using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Easypark_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
[ApiController]
public class GarageController : ControllerBase
{
    private readonly GarageServices _services;
    private readonly GarageRepo _garageRepo;
    public GarageController(GarageServices services, GarageRepo garageRepo)
    {
        _services = services;
        _garageRepo = garageRepo;
    }
   
    [HttpGet]
    [Route("/parkings")]
    public async Task<IActionResult> test()
    {
        var garages = await _services.getAsyncAllGarages();
        return Ok(garages);
    }
    [HttpPost]
    [Route("/parkings")]
    public async Task<IActionResult> InsertGarageAsync([FromBody]GarageModel garage)
    {
        try
        {
            if (garage == null)
            {
                return BadRequest("Garage model cannot be null.");
            }

            await _garageRepo.InsertGarageAsync(garage);
            return Ok(garage.GarageId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
