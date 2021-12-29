using System.IO;

namespace DatasetConverter.Common
{
    /// <summary>
    /// Класс строковых констант
    /// </summary>
    internal class StringConstants
    {
        /// <summary>
        /// Путь до папки датасета
        /// </summary>
        private static string _datasetsFolder = "DataSet";

        /// <summary>
        /// Файл со списком вылетающих авиарейсов
        /// </summary>
        public static string DepartureFlights = Path.Combine(_datasetsFolder, "Departures.txt");

        /// <summary>
        /// Файл со списком прилетающих авиарейсов
        /// </summary>
        public static string ArrivalsFlights = Path.Combine(_datasetsFolder, "Arrivals.txt");

        /// <summary>
        /// Файл со списком погодных данных METAR
        /// </summary>
        public static string RawMetars = Path.Combine(_datasetsFolder, "RawMetars.txt");
    }
}
