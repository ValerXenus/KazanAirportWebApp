using System;

namespace DatasetConverter.Objects
{
    internal class WeatherItem
    {
        /// <summary>
        /// Дата и время наблюдений
        /// </summary>
        public DateTime DateTime { get; set; }

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
    }
}
