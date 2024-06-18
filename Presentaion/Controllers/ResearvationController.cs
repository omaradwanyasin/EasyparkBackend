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
    public async Task<IActionResult> GetReservations(string garageId )
    {
        var result = await _repo.GetReservationByIdAsync(garageId);
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
    [HttpDelete("deleteReservation")]
    public async Task<IActionResult> DeleteReservation( string reservationid)
    {
        var result = await _repo.deleteRes(reservationid);
        return Ok(result);
    }
}
