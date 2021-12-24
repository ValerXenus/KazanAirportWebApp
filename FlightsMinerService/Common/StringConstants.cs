namespace GetFlightsService.Common
{
    /// <summary>
    /// Строковые константы
    /// </summary>
    internal class StringConstants
    {
        /// <summary>
        /// URL до получения списка авиарейсов
        /// </summary>
        public static string YandexScheduleApiUrl = "https://api.rasp.yandex.net/v3.0/schedule/?apikey={apiKey}&station=kzn&lang=ru_RU&format=json&transport_types=plane&system=iata&event={event}&date={date}";
    }
}
