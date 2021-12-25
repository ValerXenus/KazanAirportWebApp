using System.Collections.Generic;
using GetFlightsService.Common;
using GetFlightsService.Objects;

namespace GetFlightsService.Logic
{
    /// <summary>
    /// Получение списка авиарейсов из онлайн-табло
    /// </summary>
    internal class DashboardFlightsGetter
    {
        /// <summary>
        /// Настройки приложения
        /// </summary>
        private string _chromePath;

        /// <summary>
        /// Список вылетающих рейсов
        /// </summary>
        private List<DashboardFlight> _departureFlights;

        /// <summary>
        /// Список прилетающих рейсов
        /// </summary>
        private List<DashboardFlight> _arrivalFlights;

        public DashboardFlightsGetter(string chromePath)
        {
            _chromePath = chromePath;
        }

        /// <summary>
        /// Получить список авиарейсов из онлайн-табло
        /// </summary>
        public void ProcessFlights()
        {
            var departuresHtml = loadFlightsFromWebsite(DirectionType.Departure);
            var arrivalsHtml = loadFlightsFromWebsite(DirectionType.Arrival);
            var htmlParser = new HtmlParser(departuresHtml, arrivalsHtml);
            _departureFlights = htmlParser.GetFlights(DirectionType.Departure);
            _arrivalFlights = htmlParser.GetFlights(DirectionType.Arrival);
        }

        public List<DashboardFlight> GetDepartureFlights()
        {
            return _departureFlights;
        }

        public List<DashboardFlight> GetArrivalFlights()
        {
            return _arrivalFlights;
        }

        /// <summary>
        /// Получить HTML-контент страницы онлайн-табло из Сайта
        /// </summary>
        /// <param name="mode">Режим чтения (прилет/вылет)</param>
        /// <returns></returns>
        private string loadFlightsFromWebsite(DirectionType mode)
        {
            string flightsUrl;
            switch (mode)
            {
                case DirectionType.Departure:
                    flightsUrl = StringConstants.DeparturesUrl;
                    break;
                case DirectionType.Arrival:
                    flightsUrl = StringConstants.ArrivalsUrl;
                    break;
                default:
                    return string.Empty;
            }

            var jsContent = "var selector = document.getElementsByClassName(\"resizeselect\"); " +
                            "var event = new Event(\"change\"); " +
                            "selector[1].value = 1; " +
                            "selector[1].dispatchEvent(event);";
            using (var browser = new HeadlessBrowser(_chromePath))
            {
                return browser.GetHtmlContent(flightsUrl, jsContent);
            }
        }
    }
}
