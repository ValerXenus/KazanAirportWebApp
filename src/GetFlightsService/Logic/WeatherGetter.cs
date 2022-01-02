using System;
using System.IO;
using System.Net;
using GetFlightsService.Common;
using Serilog;

namespace GetFlightsService.Logic
{
    /// <summary>
    /// Класс для получения и обработки погодных данных
    /// </summary>
    internal class WeatherGetter
    {
        /// <summary>
        /// URL для получения погодных данных METAR
        /// </summary>
        private string _metarLink;

        public WeatherGetter()
        {
            _metarLink = StringConstants.MetarUrl.Replace("{request_date}", DateTime.Now.ToString("yyyy-MM-dd"));
        }

        #region Public methods

        public string GetWeatherInfo()
        {
            var rawMetar = sendRequest(_metarLink);
            return rawMetar;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="requestUrl">URL к API Яндекса</param>
        /// <returns></returns>
        private string sendRequest(string requestUrl)
        {
            Log.Information("Отправка запроса на получение погодных данных METAR");
            var request = WebRequest.Create(requestUrl);
            var previousLine = string.Empty;

            using (var response = request.GetResponse())
            {
                Log.Information("Ответ успешно получен");

                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return string.Empty;

                    using (var reader = new StreamReader(stream))
                    {
                        // Забираем последнюю строку (самое последнее наблюдение)
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (string.IsNullOrEmpty(line.Trim()))
                                continue;

                            previousLine = line;
                        }
                    }
                }
            }

            Log.Information("Ответ успешно прочитан");
            return previousLine;
        }

        #endregion
    }
}
