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
    public GarageController(GarageServices services)
    {
        _services = services;
    }
   
    [HttpGet]
    [Route("/parkings")]
    public async Task<IActionResult> test()
    {
        var garages = await _services.getAsyncAllGarages();
        return Ok(garages);
    }
}
