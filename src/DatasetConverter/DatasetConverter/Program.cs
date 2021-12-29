using DatasetConverter.FilesAccess;

namespace DatasetConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var departureFlights = FilesReader.Instance().DepartureFlights;
            var arrivalFlights = FilesReader.Instance().ArrivalFlights;
            var weatherList = FilesReader.Instance().WeatherList;

            var dataMapper = new DataMapper(departureFlights, arrivalFlights, weatherList);
        }
    }
}
