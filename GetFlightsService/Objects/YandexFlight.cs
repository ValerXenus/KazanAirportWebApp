namespace GetFlightsService.Objects
{
    /// <summary>
    /// Информация об авиарейсе из Яндекс.Расписание
    /// </summary>
    internal class YandexFlight
    {
        /// <summary>
        /// Номер авиарейса
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Название модели самолета
        /// </summary>
        public string PlaneName { get; set; }
    }
}
