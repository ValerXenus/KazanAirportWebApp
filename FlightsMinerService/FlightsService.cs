using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
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

        #endregion
        
        public FlightsService()
        {
            InitializeComponent();

            _scheduleInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ThreadSleepTimeInMin"]);
            _logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            _flightsFilePath = ConfigurationManager.AppSettings["OutputFilePath"];
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

        /// <summary>
        /// Получение списка авиарейсов каждые _scheduleInterval минут
        /// </summary>
        private void getFlights()
        {
            while (true)
            {
                using (var writer = new StreamWriter(_flightsFilePath, true))
                {
                    writer.WriteLine(string.Format("Windows Service Called on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
                Thread.Sleep(_scheduleInterval * 60 * 1000);
            }
        }
    }
}
