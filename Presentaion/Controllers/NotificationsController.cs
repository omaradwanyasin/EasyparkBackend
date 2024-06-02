using Easypark_Backend.Presentaion.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public NotificationController(IHubContext<NotificationHub> notificationHubContext)
        {
            _notificationHubContext = notificationHubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDto notificationDto)
        {
            // Use notificationDto.userId, notificationDto.reservationId, etc.
            await _notificationHubContext.Clients.All.SendAsync("ReceiveNotification", notificationDto.Message);
            return Ok(new { Message = "Notification sent successfully." });
        }

    }
}
