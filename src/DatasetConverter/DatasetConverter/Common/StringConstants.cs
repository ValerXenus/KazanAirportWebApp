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
        /// Папка, содержащая сформированные датасеты
        /// </summary>
        private static string _outcomeFolder = "Outcome";

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

        /// <summary>
        /// Файл готового датасета со списком вылетающих авиарейсов
        /// </summary>
        public static string DepartureDataset = Path.Combine(_outcomeFolder, "Departures.csv");

        /// <summary>
        /// Файл готового датасета со списком прилетающих авиарейсов
        /// </summary>
        public static string ArrivalDataset = Path.Combine(_outcomeFolder, "Arrivals.csv");

        /// <summary>
        /// Файл-справочник авиакомпаний
        /// </summary>
        public static string AirlinesDirectory = Path.Combine(_outcomeFolder, "AirlinesDirectory.txt");

        /// <summary>
        /// Файл-справочник городов (аэропортов)
        /// </summary>
        public static string CitiesDirectory = Path.Combine(_outcomeFolder, "CitiesDirectory.txt");
    }
}
