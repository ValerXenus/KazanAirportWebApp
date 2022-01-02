using System;
using DatasetConverter.FilesAccess;

namespace DatasetConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Files reading...");
            var departureFlights = FilesReader.Instance().DepartureFlights;
            var arrivalFlights = FilesReader.Instance().ArrivalFlights;
            var weatherList = FilesReader.Instance().WeatherList;
            Console.WriteLine("Files successfully readed and decoded.");
            
            Console.WriteLine("Mapping files...");
            var dataMapper = new DataMapper(departureFlights, arrivalFlights, weatherList);
            Console.WriteLine("Files mapped successfully");

            Console.WriteLine("Save datasets in \"Output\" folder...");
            FilesSaver.Instance().SaveFiles(dataMapper);
            Console.WriteLine("Datasets successfully saved.");
        }
    }
}
