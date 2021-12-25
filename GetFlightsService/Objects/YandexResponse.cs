using Newtonsoft.Json;

namespace GetFlightsService.Objects
{
    /// <summary>
    /// Класс ответа из API Яндекс.Расписания
    /// </summary>
    internal class YandexResponse
    {
        /// <summary>
        /// Список авиарейсов
        /// </summary>
        [JsonProperty("schedule")]
        public YandexSchedule[] Schedules { get; set; }
    }

    internal class YandexSchedule
    {
        /// <summary>
        /// Расширенная информация о рейсе
        /// </summary>
        [JsonProperty("thread")]
        public YandexThread YandexThread { get; set; }
    }

    internal class YandexThread
    {
        /// <summary>
        /// Номер авиарейса
        /// </summary>
        [JsonProperty("number")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Модель самолета
        /// </summary>
        [JsonProperty("vehicle")]
        public string Vehicle { get; set; }
    }
}
