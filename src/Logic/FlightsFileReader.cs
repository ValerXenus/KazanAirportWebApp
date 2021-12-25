using System.Collections.Generic;
using System.Configuration;
using System.IO;
using KazanAirportWebApp.Models.JoinModels;
using Newtonsoft.Json;

namespace KazanAirportWebApp.Logic
{
    /// <summary>
    /// Класс считывания данных об авиарейсах из FlightsList.json
    /// </summary>
    internal class FlightsFileReader
    {
        /// <summary>
        /// Путь до JSON-файла со списком авиарейсов
        /// </summary>
        private string _flightsFilePath;

        /// <summary>
        /// Полный список всех авиарейсов
        /// </summary>
        private FullFlights _fullFlights;

        public FlightsFileReader()
        {
            _flightsFilePath = ConfigurationManager.AppSettings["FlightsFilePath"];
        }

        #region Public methods

        /// <summary>
        /// Получить список вылетающих рейсов
        /// </summary>
        /// <returns></returns>
        public List<FlightItem> GetDepartureFlights()
        {
            reloadFlightsList();
            return _fullFlights?.DepartureFlights ?? new List<FlightItem>();
        }

        /// <summary>
        /// Получить список прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        public List<FlightItem> GetArrivalFlights()
        {
            reloadFlightsList();
            return _fullFlights?.ArrivalFlights ?? new List<FlightItem>();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Перезагрузить список авиарейсов
        /// </summary>
        private void reloadFlightsList()
        {
            var jsonContent = File.ReadAllText(_flightsFilePath);
            _fullFlights = JsonConvert.DeserializeObject<FullFlights>(jsonContent);
        }

        #endregion
    }
}