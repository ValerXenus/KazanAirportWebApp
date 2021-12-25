using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using GetFlightsService.Common;
using GetFlightsService.Logic;
using GetFlightsService.Objects;
using Newtonsoft.Json;
using Serilog;

namespace GetFlightsService
{
    public partial class FlightsService : ServiceBase
    {
        #region Private fields

        /// <summary>
        /// Интервал обновления списка авиарейсов
        /// </summary>
        private int _scheduleInterval;

        /// <summary>
        /// Фоновый процесс
        /// </summary>
        private Thread _processThread;

        /// <summary>
        /// Путь до файла с логом
        /// </summary>
        private string _logFilePath;

        /// <summary>
        /// Путь до файла со списком авиарейсов
        /// </summary>
        private string _flightsFilePath;

        /// <summary>
        /// Путь до папки с Google Chrome
        /// </summary>
        private string _chromePath;

        #endregion
        
        public FlightsService()
        {
            InitializeComponent();

            _scheduleInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ThreadSleepTimeInMin"]);
            _logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            _flightsFilePath = ConfigurationManager.AppSettings["OutputFilePath"];
            _chromePath = ConfigurationManager.AppSettings["ChromePath"];
        }

        /// <summary>
        /// Событие запуска сервиса
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            var logsFolder = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(logsFolder) && !string.IsNullOrEmpty(logsFolder))
                Directory.CreateDirectory(logsFolder);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(_logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Инициализация сервиса");

                var start = new ThreadStart(getFlights);
                _processThread = new Thread(start);
                _processThread.Start();
                
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Произошла ошибка при инициализации потока обработки. Подробности: ");
            }
        }

        /// <summary>
        /// Событие остановки сервиса
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                if (_processThread != null && _processThread.IsAlive)
                    _processThread.Abort();

                Log.Information("Завершение работы сервиса");
                Log.CloseAndFlush();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Произошла ошибка при остановке сервиса. Подробности: ");
            }
        }

        #region Private methods

        /// <summary>
        /// Получение списка авиарейсов каждые _scheduleInterval минут
        /// </summary>
        private void getFlights()
        {
            while (true)
            {
                getAndSaveFlightsData();
                Thread.Sleep(_scheduleInterval * 60 * 1000);
            }
        }

        /// <summary>
        /// Получить списки авиарейсов и сохранить
        /// </summary>
        /// <returns></returns>
        private void getAndSaveFlightsData()
        {
            try
            {
                var yandexGetter = new YandexFlightsGetter();
                var dashboardGetter = new DashboardFlightsGetter(_chromePath);
                dashboardGetter.ProcessFlights();

                Log.Information("Получение списка вылетающих рейсов");
                var yandexDepartureFlights = yandexGetter.GetFlights(DirectionType.Departure);
                var dashboardDepartureFlights = dashboardGetter.GetDepartureFlights();
                var departureFlights = mergeFlightLists(yandexDepartureFlights, dashboardDepartureFlights);

                Log.Information("Получение списка прилетающих рейсов");
                var yandexArrivalFlights = yandexGetter.GetFlights(DirectionType.Arrival);
                var dashboardArrivalFlights = dashboardGetter.GetArrivalFlights();
                var arrivalFlights = mergeFlightLists(yandexArrivalFlights, dashboardArrivalFlights);

                var flights = new
                {
                    DepartureFlights = departureFlights,
                    ArrivalFlights = arrivalFlights
                };

                Log.Information("Получение списка рейсов в файл");
                using (var file = File.CreateText(_flightsFilePath))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, flights);
                }
                Log.Information("Файл с авиарейсами успешно сохранен");
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Ошибка!");
                throw;
            }
        }

        /// <summary>
        /// Смержить списки из Яндекс.Расписания и Онлайн-табло
        /// </summary>
        /// <param name="yandexFlights">Список авиарейсов из Яндекс.Расписания</param>
        /// <param name="dashboardFlights">Список авиарейсов из Онлайн-табло</param>
        /// <returns></returns>
        private List<FlightItem> mergeFlightLists(List<YandexFlight> yandexFlights,
            List<DashboardFlight> dashboardFlights)
        {
            var outcome = new List<FlightItem>();
            foreach (var yandexFlight in yandexFlights)
            {
                var dashboardFlight = dashboardFlights.FirstOrDefault(x => x.FlightNumber == yandexFlight.FlightNumber);
                if (dashboardFlight == null)
                    continue;

                outcome.Add(new FlightItem
                {
                    FlightNumber = dashboardFlight.FlightNumber,
                    PlaneName = yandexFlight.PlaneName,
                    Destination = dashboardFlight.Destination,
                    Airline = dashboardFlight.Airline,
                    ScheduledDateTime = dashboardFlight.ScheduledDateTime,
                    ActualDateTime = dashboardFlight.ActualDateTime,
                    Status = dashboardFlight.Status
                });
            }

            Log.Information("Список авиарейсов успешно смержен");
            return outcome;
        }

        #endregion
    }
}
