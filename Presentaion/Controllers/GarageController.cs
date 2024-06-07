using Easypark_Backend.Data.DataModels;
using Easypark_Backend.Data.Repository;
using Easypark_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<IActionResult> InsertGarageAsync([FromBody] GarageModel garage)
    {
        try
        {
            if (garage == null)
            {
                return BadRequest("Garage model cannot be null.");
            }

            await _garageRepo.InsertGarageAsync(garage);
            return Ok(garage.garageOwnerId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("/parkings/{garageId}")]
    public async Task<IActionResult> GetGarageByIdAsync(string garageId)
    {
        try
        {
            if (string.IsNullOrEmpty(garageId))
            {
                return BadRequest("Garage ID cannot be null or empty.");
            }

            var garage = await _garageRepo.GetGarageByIdAsync(garageId);

            if (garage == null)
            {
                return NotFound($"Garage with ID {garageId} not found.");
            }

            return Ok(garage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
