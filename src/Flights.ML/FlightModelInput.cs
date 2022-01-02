namespace Flights.ML
{
    /// <summary>
    /// Класс данных об авиарейсе для предсказания времени задержки
    /// </summary>
    public class FlightModelInput
    {
        /// <summary>
        /// ID времени суток
        /// </summary>
        public int DayTime { get; set; }

        /// <summary>
        /// Скорость ветра (м/с)
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Дальность горизонтальной видимости (м)
        /// </summary>
        public int Visibility { get; set; }

        /// <summary>
        /// Атмосферное давление (Гектапаскали)
        /// </summary>
        public int AirPressure { get; set; }

        /// <summary>
        /// Температура воздуха (в Цельсиях)
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// ID авиакомпании
        /// </summary>
        public int AirlineId { get; set; }

        /// <summary>
        /// ID города
        /// </summary>
        public int CityId { get; set; }
    }
}
