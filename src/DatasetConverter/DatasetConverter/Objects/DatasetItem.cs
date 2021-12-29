using DatasetConverter.Enums;

namespace DatasetConverter.Objects
{
    /// <summary>
    /// Запись данными об авиарейсе
    /// </summary>
    internal class DatasetItem
    {
        /// <summary>
        /// Время суток
        /// </summary>
        public DayTime DayTime { get; set; }

        /// <summary>
        /// Скорость ветра
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Видимость
        /// </summary>
        public int Visibility { get; set; }

        /// <summary>
        /// Атмосферное давление
        /// </summary>
        public int AirPressure { get; set; }

        /// <summary>
        /// Температура воздуха
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// ID авиакомпании
        /// </summary>
        public int AirlineId { get; set; }

        /// <summary>
        /// ID города вылета/прибытия
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Время задержки в минутах
        /// </summary>
        public int DelayTime { get; set; }
    }
}
