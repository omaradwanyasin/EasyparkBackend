using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;

    public ReservationController(IHubContext<NotificationHub> notificationHubContext)
    {
        _notificationHubContext = notificationHubContext;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateReservation([FromBody] ReservationModel update)
    {
        // Perform update logic...

        // Send notification
        var message = "Your reservation has been updated.";
        await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification", message);

        return Ok();
    }
}
