using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DatasetConverter.Common;
using DatasetConverter.Objects;
using MetarParserCore;

namespace DatasetConverter.FilesAccess
{
    /// <summary>
    /// Класс чтения сырых датасетов из файлов
    /// </summary>
    internal class FilesReader
    {
        #region Fields

        /// <summary>
        /// Инстанс класса
        /// </summary>
        private static FilesReader _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Список вылетающих рейсов
        /// </summary>
        public List<FlightItem> DepartureFlights { get; }

        /// <summary>
        /// Список прилетающих рейсов
        /// </summary>
        public List<FlightItem> ArrivalFlights { get; }

        /// <summary>
        /// Список погодных данных
        /// </summary>
        public List<WeatherItem> WeatherList { get; }

        #endregion

        private FilesReader()
        {
            DepartureFlights = loadDepartureFlights();
            ArrivalFlights = loadArrivalFlights();

            // Берем стартовый месяц с начала датасета
            var currentMonth = DepartureFlights.First().ScheduledTime.Month;
            WeatherList = getWeatherList(currentMonth);
        }

        #region Public methods

        public static FilesReader Instance()
        {
            return _instance ??= new FilesReader();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Загрузить список вылетающих рейсов из файла
        /// </summary>
        /// <returns></returns>
        private List<FlightItem> loadDepartureFlights()
        {
            return loadFlightsList(StringConstants.DepartureFlights);
        }

        /// <summary>
        /// Загрузить список прилетющих рейсов из файла
        /// </summary>
        /// <returns></returns>
        private List<FlightItem> loadArrivalFlights()
        {
            return loadFlightsList(StringConstants.ArrivalsFlights);
        }

        /// <summary>
        /// Загрузить список авиарейсов из файла
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <returns></returns>
        private List<FlightItem> loadFlightsList(string filePath)
        {
            var outcome = new List<FlightItem>();
            using var flightsFile = new StreamReader(filePath);

            while (!flightsFile.EndOfStream)
            {
                var line = flightsFile.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                var parts = line.Split("|");
                if (parts.Length < 6)
                    throw new FileLoadException();

                outcome.Add(new FlightItem
                {
                    AirlineName = cleanAirlineName(parts[1]),
                    CityName = cleanCityName(parts[3]),
                    ScheduledTime = DateTime.ParseExact(parts[4], "dd.MM.yyyy HH:mm:ss", null),
                    ActualTime = DateTime.ParseExact(parts[5], "dd.MM.yyyy HH:mm:ss", null)
                });
            }

            return outcome;
        }

        /// <summary>
        /// Получить список погодных данных
        /// </summary>
        /// <param name="monthStart">Стартовый номер месяца</param>
        /// <returns></returns>
        private List<WeatherItem> getWeatherList(int monthStart)
        {
            var outcome = new List<WeatherItem>();
            var currentMonth = monthStart;
            var isMonthIncremented = false;
            using var metarFile = new StreamReader(StringConstants.RawMetars);

            while (!metarFile.EndOfStream)
            {
                var line = metarFile.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                var weatherItem = convertWeatherItem(line, ref currentMonth, ref isMonthIncremented);
                if (weatherItem == null)
                    continue;

                outcome.Add(weatherItem);
            }

            return outcome;
        }

        /// <summary>
        /// Конвертировать погодную сводку METAR в объект WeatherItem
        /// </summary>
        /// <param name="row">Строка METAR</param>
        /// <param name="month">Текущий номер месяца</param>
        /// <param name="isMonthIncremented">Признак того, что месяц уже был прибавлен</param>
        /// <returns></returns>
        private WeatherItem convertWeatherItem(string row, ref int month, ref bool isMonthIncremented)
        {
            var metarParser = new MetarParser();
            var decodedMetar = metarParser.Parse(row);
            if (decodedMetar.IsNil)
                return null;

            if (decodedMetar.ObservationDayTime.Day == 1 && !isMonthIncremented)
            {
                month++;
                isMonthIncremented = true;
            }

            if (decodedMetar.ObservationDayTime.Day == 2 && isMonthIncremented)
                isMonthIncremented = false;

            return new WeatherItem
            {
                DateTime = new DateTime(2021, month, 
                    decodedMetar.ObservationDayTime.Day, 
                    decodedMetar.ObservationDayTime.Time.Hours, 
                    decodedMetar.ObservationDayTime.Time.Minutes, 0),
                WindSpeed = decodedMetar.SurfaceWind.Speed,
                Visibility = decodedMetar.PrevailingVisibility.IsCavok 
                    ? 9999 
                    : decodedMetar.PrevailingVisibility.VisibilityInMeters.VisibilityValue,
                AirPressure = decodedMetar.AltimeterSetting.Value,
                Temperature = decodedMetar.Temperature.Value
            };
        }

        /// <summary>
        /// Чистка наименований авиакомпаний
        /// </summary>
        /// <param name="airlineName">Наименование авиакомпании</param>
        /// <returns></returns>
        private string cleanAirlineName(string airlineName)
        {
            var parts = airlineName.Split(' ');
            if (parts[0].ToLower().Equals("ао") || parts[0].ToLower().Equals("пао"))
                return string.Join(' ', parts[1..]);

            return airlineName;
        }

        /// <summary>
        /// Чистка наименования города
        /// </summary>
        /// <param name="cityName">Наименование города</param>
        /// <returns></returns>
        private string cleanCityName(string cityName)
        {
            if (cityName.ToLower().StartsWith("санкт"))
                return "Санкт-Петербург";

            if (cityName.ToLower().StartsWith("саратов"))
                return "Саратов";

            return cityName;
        }

        #endregion
    }
}
