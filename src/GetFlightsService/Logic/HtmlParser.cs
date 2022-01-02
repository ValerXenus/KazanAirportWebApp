using System.Collections.Generic;
using GetFlightsService.Common;
using GetFlightsService.Objects;
using HtmlAgilityPack;
using Serilog;

namespace GetFlightsService.Logic
{
    /// <summary>
    /// Класс парсинга HTML
    /// </summary>
    internal class HtmlParser
    {
        /// <summary>
        /// HTML-документ Departures
        /// </summary>
        private readonly HtmlDocument _departuresDocument;

        /// <summary>
        /// HTML-документ Arrivals
        /// </summary>
        private readonly HtmlDocument _arrivalsDocument;

        /// <summary>
        /// Парсер списка рейсов
        /// </summary>
        private readonly DashboardFlightsParser _flightsParser;


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="departuresHtml">HTML содержимое вылетов в виде строки</param>
        /// <param name="arrivalsHtml">HTML содержимое прилетов в виде строки</param>
        public HtmlParser(string departuresHtml, string arrivalsHtml)
        {
            Log.Information("Инициализация HTML парсера");

            _departuresDocument = new HtmlDocument();
            _departuresDocument.LoadHtml(departuresHtml);

            _arrivalsDocument = new HtmlDocument();
            _arrivalsDocument.LoadHtml(arrivalsHtml);

            _flightsParser = new DashboardFlightsParser();
            Log.Information("HTML парсер успешно инициализирован");
        }

        /// <summary>
        /// Главный парсер списка авиарейсов
        /// </summary>
        /// <param name="directionType">Тип рейса (Departure/Arrival)</param>
        /// <returns></returns>
        public List<DashboardFlight> GetFlights(DirectionType directionType)
        {
            var logWord = "ВЫЛЕТАЮЩИХ";
            var flightsDocument = _departuresDocument;

            if (directionType == DirectionType.Arrival)
            {
                logWord = "ПРИЛЕТАЮЩИХ";
                flightsDocument = _arrivalsDocument;
            }

            Log.Information($"Запуск парсинга списка {logWord} рейсов");
            var nodes = flightsDocument.DocumentNode.SelectNodes(StringConstants.FlightsXPath);
            return nodes == null
                ? new List<DashboardFlight>()
                : _flightsParser.ParseFlightsRows(nodes, directionType);
        }
    }
}
