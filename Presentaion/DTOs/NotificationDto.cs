namespace Easypark_Backend.Presentaion.DTOs
{
    public class NotificationDto
    {
        public string UserId { get; set; }
        public string ReservationId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
