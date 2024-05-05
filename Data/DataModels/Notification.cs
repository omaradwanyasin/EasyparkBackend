namespace Easypark_Backend.Data.DataModels
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int userID { get; set; }
    }
}
