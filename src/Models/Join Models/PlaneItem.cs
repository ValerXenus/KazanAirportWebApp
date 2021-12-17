namespace KazanAirportWebApp.Models.Join_Models
{
    /// <summary>
    /// Информация о самолете (ВС)
    /// </summary>
    public class PlaneItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int SeatsNumber { get; set; }
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
    }
}