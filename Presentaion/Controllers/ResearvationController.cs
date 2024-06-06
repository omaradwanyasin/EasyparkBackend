using Easypark_Backend.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly ReservationRepo _repo;

    public ReservationController(ReservationRepo repo)
    {
        _repo = repo;
    }

    [HttpGet("reservation")]
    public async Task<IActionResult> GetReservations()
    {
        var result = await _repo.getReservations();
        return Ok(result);
    }

    [HttpPost("AddReservation")]
    public async Task<IActionResult> AddReservation([FromBody] ReservationModel ob)
    {
        if (ob == null)
        {
            return BadRequest("Reservation data is null");
        }

        var result = await _repo.addreservation(ob);
        return Ok(result);
    }
}
