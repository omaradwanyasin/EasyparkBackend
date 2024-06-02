using Microsoft.AspNetCore.Mvc;
using Easypark_Backend.Data.Repository;
using Microsoft.Extensions.Logging;

namespace Easypark_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationsRepo _notificationsRepo;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(NotificationsRepo notificationsRepo, ILogger<NotificationsController> logger)
        {
            _notificationsRepo = notificationsRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            if (notification == null)
            {
                _logger.LogError("Notification object is null");
                return BadRequest(new { message = "Notification object is null" });
            }

            _logger.LogInformation("Received notification: {Notification}", notification);

            if (string.IsNullOrEmpty(notification.UserId))
            {
                _logger.LogError("UserId is required");
                return BadRequest(new { message = "UserId is required" });
            }

            if (string.IsNullOrEmpty(notification.ReservationId))
            {
                _logger.LogError("ReservationId is required");
                return BadRequest(new { message = "ReservationId is required" });
            }

            if (string.IsNullOrEmpty(notification.Message))
            {
                _logger.LogError("Message is required");
                return BadRequest(new { message = "Message is required" });
            }

            if (notification.CreatedAt == default(DateTime))
            {
                _logger.LogError("CreatedAt is required");
                return BadRequest(new { message = "CreatedAt is required" });
            }

            try
            {
                await _notificationsRepo.CreateAsync(notification);
                _logger.LogInformation("Notification created successfully: {Notification}", notification);
                return Ok(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating the notification: {Error}", ex.Message);
                return StatusCode(500, new { message = "An error occurred while creating the notification", details = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(string userId)
        {
            var notifications = await _notificationsRepo.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }
    }
}
