namespace Easypark_Backend.Data.DataModels
{
    public class ReservationModel:GarageOwnerModels
    {
        public int ReversationId { get; set; }
        public int GarageId { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }    
        public int PhoneNumber { get; set; }
        public string ImgURL { get; set; }

        

    }
}
