namespace KazanAirportWebApi.Models.JoinModels
{
    /// <summary>
    /// Строка топа авиакомпаний
    /// </summary>
    public class AirlineTopItem
    {
        /// <summary>
        /// Наименование авиакомпании
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Среднее время задержки авиарейсов
        /// </summary>
        public double DelayTime { get; set; }
    }
}
