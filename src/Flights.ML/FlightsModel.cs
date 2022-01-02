using System;
using Flights_ML;

namespace Flights.ML
{
    /// <summary>
    /// Класс для работы с ML-моделями
    /// </summary>
    public class FlightsModel
    {
        private static FlightsModel _instance;

        #region Public methods

        public static FlightsModel Instance()
        {
            return _instance ??= new FlightsModel();
        }

        /// <summary>
        /// Получить время задержки вылета авиарейса
        /// </summary>
        /// <param name="modelInput">Данные об авиарейсе</param>
        /// <returns></returns>
        public int GetDeparturePrediction(FlightModelInput modelInput)
        {
            var sampleData = new DeparturesModel.ModelInput
            {
                DayTime = modelInput.DayTime,
                WindSpeed = modelInput.WindSpeed,
                Visibility = modelInput.Visibility,
                AirPressure = modelInput.AirPressure,
                Temperature = modelInput.Temperature,
                AirlineId = modelInput.AirlineId,
                CityId = modelInput.CityId,
            };

            var result = DeparturesModel.Predict(sampleData);
            return (int) Math.Round(result.Score);
        }

        /// <summary>
        /// Получить время задержки прилета авиарейса
        /// </summary>
        /// <param name="modelInput">Данные об авиарейсе</param>
        /// <returns></returns>
        public int GetArrivalPrediction(FlightModelInput modelInput)
        {
            var sampleData = new ArrivalsModel.ModelInput
            {
                DayTime = modelInput.DayTime,
                WindSpeed = modelInput.WindSpeed,
                Visibility = modelInput.Visibility,
                AirPressure = modelInput.AirPressure,
                Temperature = modelInput.Temperature,
                AirlineId = modelInput.AirlineId,
                CityId = modelInput.CityId,
            };

            var result = ArrivalsModel.Predict(sampleData);
            return (int)Math.Round(result.Score);
        }

        #endregion
    }
}
