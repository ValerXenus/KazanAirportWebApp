namespace GetFlightsService.Common
{
    /// <summary>
    /// Строковые константы
    /// </summary>
    internal class StringConstants
    {
        /// <summary>
        /// URL для получения списка авиарейсов
        /// </summary>
        public static string YandexScheduleApiUrl = "https://api.rasp.yandex.net/v3.0/schedule/?apikey={apiKey}&station=kzn&lang=ru_RU&format=json&transport_types=plane&system=iata&event={event}&date={date}";
        
        /// <summary>
        /// URL для получения данных METAR
        /// </summary>
        public static string MetarUrl = "https://api.met.no/weatherapi/tafmetar/1.0/metar.txt?date={request_date}&offset=+03:00&icao=UWKD";

        /// <summary>
        /// Ссылка на страницу со списком вылетающих рейсов
        /// </summary>
        public static string DeparturesUrl = "https://kazan.aero/on-line-schedule/";

        /// <summary>
        /// Ссылка на страницу со списком прилетающих рейсов
        /// </summary>
        public static string ArrivalsUrl = "https://kazan.aero/on-line-schedule/arrival/";

        /// <summary>
        /// XPath до элементов таблицы "Вылет"
        /// </summary>
        public static string FlightsXPath =
            "//body/div[@class='cont']/div[2]/div[@class='main-content__template']/div[@class='flights-shedule']/div[@class='shedule__items']/div";

        /// <summary>
        /// Путь до таблицы с данными о рейсе внутри узла рейсов сайта
        /// </summary>
        public static string FlightTableXPath = "div[@class='shedule__item-table']/div/";

        /// <summary>
        /// XPath до поля "Статус рейса"
        /// </summary>
        public static string FlightStatusXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_status']");

        /// <summary>
        /// XPath до поля "Статус рейса" выделенный как предупреждение
        /// </summary>
        public static string FlightStatusWarningXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_status red_text']");

        /// <summary>
        /// XPath до поля "Номер рейса"
        /// </summary>
        public static string FlightNumberXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_number']/div/span");

        /// <summary>
        /// XPath до поля "Название авиакомпании"
        /// </summary>
        public static string AirlineXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_number']/div");

        /// <summary>
        /// XPath до поля "Город назначения"
        /// </summary>
        public static string DestinationXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_direction']");

        /// <summary>
        /// XPath узла с датами
        /// </summary>
        public static string FlightDateTimeXPath = string.Concat(FlightTableXPath, "div[@class='shedule__item-cell shedule_time']");

        /// <summary>
        /// XPath даты/времени рейса, совпадающего с расписанием
        /// </summary>
        public static string CoincidentDateTimeXPath = "div[@class='time_currect']";

        /// <summary>
        /// XPath даты/времени рейса по расписанию
        /// </summary>
        public static string ScheduledDateTimeXPath = "div[@class='time_old']";

        /// <summary>
        /// XPath реального даты/времени рейса
        /// </summary>
        public static string ActualDateTimeXPath = "div[@class='time_new']";

        /// <summary>
        /// XPath реального даты/времени рейса (Опаздывающего надолго)
        /// </summary>
        public static string ActualDateTimeLateXPath = "div[@class='time_new red_text']";
    }
}
