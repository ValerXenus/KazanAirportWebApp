using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Flights.ML;
using KazanAirportWebApi.Models.JoinModels;
using MetarParserCore;
using MetarParserCore.Objects;
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
        /// Путь до JSON-файла со списком авиарейсов
        /// </summary>
        private string _flightsFilePath;

        /// <summary>
        /// Словарь авиакомпаний
        /// </summary>
        private Dictionary<int, string> _airlinesDirectory;

        /// <summary>
        /// Словарь городов
        /// </summary>
        private Dictionary<int, string> _citiesDirectory;

        /// <summary>
        /// Рейтинг авиакомпаний
        /// </summary>
        private Dictionary<string, double> _airlinesTop;

        /// <summary>
        /// Полный список всех авиарейсов
        /// </summary>
        private FullFlights _fullFlights;

        private FlightsFileReader(IConfiguration config)
        {
            _flightsFilePath = config.GetValue<string>("FlightsFilePath");

            _airlinesDirectory = loadDictionary(config.GetValue<string>("AirlinesDirectoryFilePath"));
            _citiesDirectory = loadDictionary(config.GetValue<string>("CitiesDirectoryFilePath"));
            _airlinesTop = loadFlightsTop(config.GetValue<string>("AirlinesTopFilePath"));
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
            var departures = _fullFlights?.DepartureFlights;
            if (departures == null)
                return new List<FlightItem>();

            var metar = getWeatherInfo();

            for (var i = 0; i < departures.Count; i++)
                departures[i] = predictFlightDelayTime(departures[i], metar, 1);

            return departures;
        }

        /// <summary>
        /// Получить список прилетающих рейсов
        /// </summary>
        /// <returns></returns>
        public List<FlightItem> GetArrivalFlights()
        {
            reloadFlightsList();
            var arrivals = _fullFlights?.ArrivalFlights;
            if (arrivals == null)
                return new List<FlightItem>();

            var metar = getWeatherInfo();

            for (var i = 0; i < arrivals.Count; i++)
                arrivals[i] = predictFlightDelayTime(arrivals[i], metar, 2);

            return arrivals;
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

        /// <summary>
        /// Получить список рейтинга авиакомпаний
        /// </summary>
        /// <returns></returns>
        public List<AirlineTopItem> GetFlightsRating()
        {
            return _airlinesTop.Select(x => new AirlineTopItem
            {
                Name = x.Key,
                DelayTime = x.Value
            }).ToList();
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

        /// <summary>
        /// Предсказать приблизительное фактическое время вылета/прилета авиарейса
        /// </summary>
        /// <param name="flightItem">Информация об авиарейсе</param>
        /// <param name="metar">Погодные данные</param>
        /// <param name="directionType">Тип авиарейса, 1 - Вылет, 2 - Прилет</param>
        /// <returns></returns>
        private FlightItem predictFlightDelayTime(FlightItem flightItem, Metar metar, int directionType)
        {
            if (flightItem.StatusName.ToLower().Contains("вылетел") 
                || flightItem.StatusName.ToLower().Contains("прибыл"))
                return flightItem;

            var scheduledTime = flightItem.ScheduledDateTime;
            var airlineId = getIdxFromDictionary(_airlinesDirectory, flightItem.AirlineName);
            var cityId = getIdxFromDictionary(_citiesDirectory, flightItem.CityName);
            var visibility = metar.PrevailingVisibility.IsCavok
                ? 9999
                : metar.PrevailingVisibility.VisibilityInMeters.VisibilityValue;

            var modelInput = new FlightModelInput
            {
                DayTime = getDayTimeByScheduled(scheduledTime),
                WindSpeed = metar.SurfaceWind.Speed,
                AirPressure = metar.AltimeterSetting.Value,
                Visibility = visibility,
                Temperature = 0,
                AirlineId = airlineId,
                CityId = cityId
            };

            var delayTime = directionType == 1
                ? FlightsModel.Instance().GetDeparturePrediction(modelInput)
                : FlightsModel.Instance().GetArrivalPrediction(modelInput);

            flightItem.ActualDateTime = scheduledTime.AddMinutes(delayTime);
            flightItem.IsPredicted = true;

            return flightItem;
        }

        /// <summary>
        /// Загрузить словарь из файла
        /// </summary>
        /// <param name="filePath">Путь до файла данных словаря</param>
        /// <returns></returns>
        private Dictionary<int, string> loadDictionary(string filePath)
        {
            var outcome = new Dictionary<int, string>();

            if (!File.Exists(filePath))
                return outcome;

            using var fileReader = new StreamReader(filePath);
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                var parts = line.Split('|');
                outcome.Add(int.Parse(parts[0].Trim()), parts[1]);
            }

            return outcome;
        }

        /// <summary>
        /// Загрузить список рейтинга авиакомпаний
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <returns></returns>
        private Dictionary<string, double> loadFlightsTop(string filePath)
        {
            var outcome = new Dictionary<string, double>();

            if (!File.Exists(filePath))
                return outcome;

            using var fileReader = new StreamReader(filePath);
            while (!fileReader.EndOfStream)
            {
                var line = fileReader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                var parts = line.Split('|');
                outcome.Add(parts[0], double.Parse(parts[1].Trim()));
            }

            return outcome;
        }

        /// <summary>
        /// Получить индекс из словаря по наименованию
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="name">Наименование</param>
        /// <returns></returns>
        private int getIdxFromDictionary(Dictionary<int, string> dictionary, string name)
        {
            foreach (var element in dictionary)
            {
                if (element.Value.ToLower().Equals(name.ToLower()))
                    return element.Key;
            }

            return -1;
        }

        /// <summary>
        /// Получить время суток по дате и времени в расписании
        /// </summary>
        /// <param name="scheduledDateTime">Дата/время по расписанию</param>
        /// <returns></returns>
        private int getDayTimeByScheduled(DateTime scheduledDateTime)
        {
            return scheduledDateTime switch
            {
                { Hour: >= 0 and < 6 } => 0,
                { Hour: >= 6 and < 12 } => 1,
                { Hour: >= 12 and < 18 } => 2,
                _ => 3
            };
        }

        /// <summary>
        /// Распарсить полученный METAR
        /// </summary>
        /// <returns></returns>
        private Metar getWeatherInfo()
        {
            var metarParser = new MetarParser();
            return metarParser.Parse(_fullFlights.Metar);
        }

        #endregion
    }
}