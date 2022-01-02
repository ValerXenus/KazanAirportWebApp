using System.Collections.Generic;
using System.IO;
using System.Linq;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KazanAirportWebApi.Logic
{
    /// <summary>
    /// Класс считывания данных об авиарейсах из FlightsList.json
    /// </summary>
    internal class FlightsFileReader
    {
        /// <summary>
        /// Инстанс текущего класса
        /// </summary>
        private static FlightsFileReader _instance;

        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Путь до JSON-файла со списком авиарейсов
        /// </summary>
        private string _flightsFilePath;

        /// <summary>
        /// Полный список всех авиарейсов
        /// </summary>
        private FullFlights _fullFlights;

        private FlightsFileReader(IConfiguration config)
        {
            _config = config;
            _flightsFilePath = _config.GetValue<string>("FlightsFilePath");
        }

        #region Public methods

        /// <summary>
        /// Получить инстанс текущего класса
        /// </summary>
        /// <returns></returns>
        public static FlightsFileReader Instance(IConfiguration config)
        {
            return _instance ?? new FlightsFileReader(config);
        }

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

        /// <summary>
        /// Получить рейс из файла
        /// <param name="flightId">ID рейса в файле</param>
        /// <param name="directionType">Тип авиарейса, 1 - Вылет, 2 - Прилет</param>
        /// </summary>
        /// <returns></returns>
        public FlightItem GetFlightById(int flightId, int directionType)
        {
            reloadFlightsList();
            return directionType == 1
                ? _fullFlights.DepartureFlights.FirstOrDefault(x => x.Id == flightId)
                : _fullFlights.ArrivalFlights.FirstOrDefault(x => x.Id == flightId);
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