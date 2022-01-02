namespace KazanAirportWebApi.Models.JoinModels
{
    /// <summary>
    /// Информация о самолете (ВС)
    /// </summary>
    public class PlaneItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование модели самолета
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Бортовой номер самолета
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Количество мест в самолете
        /// </summary>
        public int SeatsNumber { get; set; }

        /// <summary>
        /// ID авиакомпании
        /// </summary>
        public int AirlineId { get; set; }

        /// <summary>
        /// Наименование авиакомпании
        /// </summary>
        public string AirlineName { get; set; }
    }
}