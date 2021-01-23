namespace KazanAirportWebApp.Models.Join_Models
{
    public class PlaneItem
    {
        public int id { get; set; }
        public string modelName { get; set; }
        public string boardNumber { get; set; }
        public int seatsNumber { get; set; }
        public int airlineId { get; set; }
        public string airlineName { get; set; }
    }
}