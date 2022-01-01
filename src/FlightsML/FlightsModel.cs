using System;

namespace FlightsML
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
            return _instance ?? (_instance = new FlightsModel());
        }

        /// <summary>
        /// Получить время задержки вылета авиарейса
        /// </summary>
        /// <param name="modelInput">Данные об авиарейсе</param>
        /// <returns></returns>
        public int GetDeparturePrediction(FlightModelInput modelInput)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить время задержки прилета авиарейса
        /// </summary>
        /// <param name="modelInput">Данные об авиарейсе</param>
        /// <returns></returns>
        public int GetArrivalPrediction(FlightModelInput modelInput)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
