namespace Easypark_Backend.Data.DataModels
{
    public class GarageModel
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageDescription { get; set; }
        public double GarageLocation { get; set; }
        public int capacity { get; set; }
    }
}
