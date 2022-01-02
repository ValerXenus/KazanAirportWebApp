using System.Collections.Generic;
using System.IO;
using DatasetConverter.Common;
using DatasetConverter.Objects;

namespace DatasetConverter.FilesAccess
{
    /// <summary>
    /// Класс для сохранения файлов
    /// </summary>
    internal class FilesSaver
    {
        private static FilesSaver _instance;

        #region Public methods

        public static FilesSaver Instance()
        {
            if (!Directory.Exists("Outcome"))
                Directory.CreateDirectory("Outcome");

            return _instance ??= new FilesSaver();
        }

        public void SaveFiles(DataMapper dataMapper)
        {
            // Сохранение датасетов авиарейсов
            saveFlightFile(dataMapper.DepartureDataset, StringConstants.DepartureDataset);
            saveFlightFile(dataMapper.ArrivalDataset, StringConstants.ArrivalDataset);

            // Сохранение справочных файлов
            saveDirectoryFile(dataMapper.AirlinesDirectory, StringConstants.AirlinesDirectory);
            saveDirectoryFile(dataMapper.CitiesDirectory, StringConstants.CitiesDirectory);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Сохранить файл датасета авиарейсов
        /// </summary>
        /// <param name="flightsDataset">Датасет авиарейсов</param>
        /// <param name="filename">Имя файла</param>
        private void saveFlightFile(List<DatasetItem> flightsDataset, string filename)
        {
            using var flightsFile = new StreamWriter(filename);
            var header = $"dayTime|windSpeed|visibility|airPressure" +
                       $"|temperature|airlineId|cityId|delayTime";
            flightsFile.WriteLine(header);

            foreach (var flight in flightsDataset)
            {
                if (flight.DelayTime > 60 || flight.DelayTime < -30)
                    continue;

                var line = $"{(int)flight.DayTime}|{flight.WindSpeed}|{flight.Visibility}|{flight.AirPressure}" +
                    $"|{flight.Temperature}|{flight.AirlineId}|{flight.CityId}|{flight.DelayTime}";
                flightsFile.WriteLine(line);
            }
        }

        /// <summary>
        /// Сохранить файл справочника
        /// </summary>
        /// <param name="directory">Данные справочника</param>
        /// <param name="filename">Имя файла</param>
        private void saveDirectoryFile(Dictionary<int, string> directory, string filename)
        {
            using var flightsFile = new StreamWriter(filename);

            foreach (var row in directory)
            {
                var line = $"{row.Key}|{row.Value}";
                flightsFile.WriteLine(line);
            }
        }

        #endregion
    }
}
