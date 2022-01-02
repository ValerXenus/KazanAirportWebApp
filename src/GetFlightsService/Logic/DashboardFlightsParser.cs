using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GetFlightsService.Common;
using GetFlightsService.Objects;
using HtmlAgilityPack;
using Serilog;

namespace GetFlightsService.Logic
{
    internal class DashboardFlightsParser
    {
        #region Public methods

        /// <summary>
        /// Парсинг списка рейсов
        /// </summary>
        /// <param name="nodes">Список HTML-узлов рейсов</param>
        /// <param name="direction">Направление полета (Вылет/Прилет)</param>
        /// <returns></returns>
        public List<DashboardFlight> ParseFlightsRows(HtmlNodeCollection nodes, DirectionType direction)
        {
            return nodes
                .Select(node => ParseFlight(node, direction))
                .Where(flight => flight != null)
                .ToList();
        }

        /// <summary>
        /// Парсинг информации о рейсе
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <param name="direction">Направление полета (Вылет/Прилет)</param>
        /// <returns></returns>
        public DashboardFlight ParseFlight(HtmlNode node, DirectionType direction)
        {
            if (!checkFlightIsToday(node))
                return null;

            var outcome = new DashboardFlight
            {
                FlightNumber = parseFlightNumber(node),
                Airline = parseAirline(node),
                Destination = parseCityName(node),
                ScheduledDateTime = parseScheduledDateTime(node, TimeType.Scheduled),
                ActualDateTime = parseScheduledDateTime(node, TimeType.Actual),
                Status = parseFlightStatus(node)
            };

            return outcome.CheckDataValid() ? outcome : null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Проверка, что полет осуществляется сегодня
        /// Отбираем только сегодняшние рейсы
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private bool checkFlightIsToday(HtmlNode node)
        {
            var flightDateInfo = node.SelectSingleNode(StringConstants.FlightDateTimeXPath);
            var realFlightDateTimeNode = flightDateInfo.SelectSingleNode(StringConstants.ActualDateTimeXPath)
                                         ?? flightDateInfo.SelectSingleNode(StringConstants.ActualDateTimeLateXPath)
                                         ?? flightDateInfo.SelectSingleNode(StringConstants.CoincidentDateTimeXPath);

            if (realFlightDateTimeNode == null)
                return false;

            var realDate = parseFlightDate(realFlightDateTimeNode.InnerText, false);

            return realDate == DateTime.Now.Date;
        }

        /// <summary>
        /// Проверка статуса полета
        /// Отбираем только вылетевшие/прилетевшие рейсы
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseFlightStatus(HtmlNode node)
        {
            var statusNode = node.SelectSingleNode(StringConstants.FlightStatusXPath)
                             ?? node.SelectSingleNode(StringConstants.FlightStatusWarningXPath);
            if (statusNode == null || string.IsNullOrEmpty(statusNode.InnerText.Trim()))
            {
                Log.Error($"Узел статуса оказался пустым. Узел рейса: {node.InnerHtml}");
                return string.Empty;
            }

            return statusNode.InnerHtml
                .Split('<')[0]
                .Trim();
        }

        /// <summary>
        /// Парсинг номера рейса
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseFlightNumber(HtmlNode node)
        {
            var flightNumber = node.SelectSingleNode(StringConstants.FlightNumberXPath).InnerText;
            return string.IsNullOrEmpty(flightNumber)
                ? string.Empty
                : flightNumber.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Парсинг авиакомпании
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseAirline(HtmlNode node)
        {
            var airlineInfo = node.SelectSingleNode(StringConstants.AirlineXPath);
            if (airlineInfo == null)
            {
                Log.Error($"Не удалось получить название авиакомпании {node.InnerText}");
                return default;
            }

            return airlineInfo.Attributes["title"].Value;
        }

        /// <summary>
        /// Парсинг города назначения
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <returns></returns>
        private string parseCityName(HtmlNode node)
        {
            var destinationName = node.SelectSingleNode(StringConstants.DestinationXPath).InnerText;
            return string.IsNullOrEmpty(destinationName)
                ? string.Empty
                : destinationName.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Парсинг даты и времени (вылета/прибытия) по расписанию
        /// </summary>
        /// <param name="node">HTML-узел рейса</param>
        /// <param name="timeType">Тип получаемого времени</param>
        /// <returns></returns>
        private DateTime parseScheduledDateTime(HtmlNode node, TimeType timeType)
        {
            var flightDateInfo = node.SelectSingleNode(StringConstants.FlightDateTimeXPath);

            HtmlNode time;
            if (timeType == TimeType.Actual)
                time = flightDateInfo.SelectSingleNode(StringConstants.ActualDateTimeXPath)
                    ?? flightDateInfo.SelectSingleNode(StringConstants.ActualDateTimeLateXPath)
                    ?? flightDateInfo.SelectSingleNode(StringConstants.CoincidentDateTimeXPath);
            else
                time = flightDateInfo.SelectSingleNode(StringConstants.ScheduledDateTimeXPath)
                       ?? flightDateInfo.SelectSingleNode(StringConstants.CoincidentDateTimeXPath);

            if (time == null)
                return DateTime.MinValue;

            var flightDateTime = time.InnerText;

            return string.IsNullOrEmpty(flightDateTime)
                ? DateTime.MinValue
                : parseFlightDate(flightDateTime);
        }

        /// <summary>
        /// Парсинг даты вылета/прилета из таблицы
        /// </summary>
        /// <param name="inputDate">Дата в виде строки</param>
        /// <param name="useDayTime">Признак необходимости парсинга времени</param>
        /// <returns></returns>
        private DateTime parseFlightDate(string inputDate, bool useDayTime = true)
        {
            inputDate = inputDate.TrimStart().TrimEnd();
            var datePattern = "dd.MM.yyyy HH:mm";

            if (!useDayTime)
            {
                datePattern = "dd.MM.yyyy";
                inputDate = inputDate.Split(' ')[0];
            }

            if (DateTime.TryParseExact(inputDate, datePattern, new CultureInfo("ru-RU"),
                DateTimeStyles.None, out var parsedDate))
                return parsedDate;

            Log.Error($"Не удалось распарсить дату: {inputDate}");
            return DateTime.MinValue;
        }
        #endregion
    }
}
