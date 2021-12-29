using System;
using System.Collections.Generic;
using System.Linq;
using DatasetConverter.Enums;
using DatasetConverter.Objects;

namespace DatasetConverter
{
    /// <summary>
    /// Маппер данных в DatasetItem
    /// </summary>
    internal class DataMapper
    {
        #region Fields

        /// <summary>
        /// Список вылетающих рейсов
        /// </summary>
        private List<FlightItem> _departureFlights;

        /// <summary>
        /// Список прилетающих рейсов
        /// </summary>
        private List<FlightItem> _arrivalFlights;

        /// <summary>
        /// Список погодных данных
        /// </summary>
        private List<WeatherItem> _weatherList;

        #endregion

        #region Properties

        /// <summary>
        /// Датасет вылетающих рейсов
        /// </summary>
        public List<DatasetItem> DepartureDataset { get; }

        /// <summary>
        /// Датасет прилетающих рейсов
        /// </summary>
        public List<DatasetItem> ArrivalDataset { get; }

        /// <summary>
        /// Справочник авиакомпаний
        /// </summary>
        public Dictionary<int, string> AirlinesDirectory { get; }

        /// <summary>
        /// Справочник городов
        /// </summary>
        public Dictionary<int, string> CitiesDirectory { get; }

        #endregion

        public DataMapper(List<FlightItem> departureFlights, List<FlightItem> arrivalFlights,
            List<WeatherItem> weatherList)
        {
            _departureFlights = departureFlights;
            _arrivalFlights = arrivalFlights;
            _weatherList = weatherList;

            // Создание справочников
            AirlinesDirectory = getAirlinesDirectory(_departureFlights.Concat(_arrivalFlights).ToList());
            CitiesDirectory = getCitiesDirectory(_departureFlights.Concat(_arrivalFlights).ToList());

            DepartureDataset = getDataset(_departureFlights);
            ArrivalDataset = getDataset(_arrivalFlights);
        }

        #region Private methods

        /// <summary>
        /// Получить справочник авиакомпаний
        /// </summary>
        /// <param name="flights">Список авиарейсов</param>
        /// <returns></returns>
        private Dictionary<int, string> getAirlinesDirectory(List<FlightItem> flights)
        {
            var airlinesDirectory = new Dictionary<int, string>();
            var airlines = flights.Select(x => normalizeDirectoryName(x.AirlineName)).Distinct().ToList();

            for (var i = 0; i < airlines.Count; i++)
                airlinesDirectory.Add(i, airlines[i]);

            return airlinesDirectory;
        }

        /// <summary>
        /// Получить справочник городов
        /// </summary>
        /// <param name="flights">Список авиарейсов</param>
        /// <returns></returns>
        private Dictionary<int, string> getCitiesDirectory(List<FlightItem> flights)
        {
            var citiesDirectory = new Dictionary<int, string>();
            var cities = flights.Select(x => normalizeDirectoryName(x.CityName)).Distinct().ToList();

            for (var i = 0; i < cities.Count; i++)
                citiesDirectory.Add(i, cities[i]);

            return citiesDirectory;
        }

        /// <summary>
        /// Получить датасет авиарейсов
        /// </summary>
        /// <param name="flights">Список авиарейсов</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<DatasetItem> getDataset(List<FlightItem> flights)
        {
            var outcome = new List<DatasetItem>();
            var dateIdx = 0;

            foreach (var flight in flights)
            {
                var weatherItem = getWeatherWithIdx(_weatherList, flight.ScheduledTime, ref dateIdx);
                var airlineId = getIdxFromDirectory(AirlinesDirectory, flight.AirlineName);
                var cityId = getIdxFromDirectory(CitiesDirectory, flight.CityName);
                var delayTime = (flight.ActualTime - flight.ScheduledTime).TotalMinutes;
                var dayTime = getDayTime(flight.ScheduledTime);

                outcome.Add(new DatasetItem
                {
                    AirlineId = airlineId,
                    AirPressure = weatherItem.AirPressure,
                    CityId = cityId,
                    DayTime = dayTime,
                    DelayTime = (int)delayTime,
                    Temperature = weatherItem.Temperature,
                    Visibility = weatherItem.Visibility,
                    WindSpeed = weatherItem.WindSpeed
                });
            }

            return outcome;
        }

        /// <summary>
        /// Получить погодные данные, и вернуть последний индекс
        /// </summary>
        /// <param name="weatherList">Погодный список</param>
        /// <param name="flightDateTime">Дата и время авиарейса</param>
        /// <param name="idx">Индекс</param>
        /// <returns></returns>
        private WeatherItem getWeatherWithIdx(List<WeatherItem> weatherList, DateTime flightDateTime, ref int idx)
        {
            // Смещение на 3 часа относительно времени Зулу (UTC)
            while (weatherList[idx].DateTime.AddHours(3) < flightDateTime)
            {
                idx++;

                if (idx >= weatherList.Count - 1)
                    break;
            }

            return weatherList[idx];
        }

        /// <summary>
        /// Получить индекс справочной записи по имени
        /// </summary>
        /// <param name="dictionary">Справочник</param>
        /// <param name="name">Наименование</param>
        /// <returns></returns>
        private int getIdxFromDirectory(Dictionary<int, string> dictionary, string name)
        {
            foreach (var record in dictionary)
            {
                if (record.Value.ToLower().Equals(name.ToLower()))
                    return record.Key;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Получить текущее время суток
        /// </summary>
        /// <param name="currentTime">Текущая дата и время</param>
        /// <returns></returns>
        private DayTime getDayTime(DateTime currentTime)
        {
            switch (currentTime)
            {
                case { Hour: >= 0 and < 6 }:
                    return DayTime.From0To6;
                case { Hour: >= 6 and < 12 }:
                    return DayTime.From6To12;
                case { Hour: >= 12 and < 18 }:
                    return DayTime.From12To18;
                default:
                    return DayTime.From18To0;
            }
        }

        /// <summary>
        /// Нормализовать наименование для справочника
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns></returns>
        private string normalizeDirectoryName(string name)
        {
            var parts = name.Split(' ');
            for (var i = 0; i < parts.Length; i++)
                parts[i] = string.Concat(parts[i][..1].ToUpper(), parts[i][1..].ToLower());

            return string.Join(' ', parts);
        }

        #endregion
    }
}
