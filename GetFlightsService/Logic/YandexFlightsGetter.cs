using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using GetFlightsService.Common;
using GetFlightsService.Objects;
using Newtonsoft.Json;
using Serilog;

namespace GetFlightsService.Logic
{
    internal class YandexFlightsGetter
    {
        /// <summary>
        /// URL списка вылетающих авиарейсов
        /// </summary>
        private string _departureFlights;

        /// <summary>
        /// URL списка прилетающих авиарейсов
        /// </summary>
        private string _arrivalFlights;

        public YandexFlightsGetter()
        {
            Log.Information("Инициализация майнера Яндекс.Расписания");

            var apiKey = ConfigurationManager.AppSettings["YandexApiKey"];
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            _departureFlights = StringConstants.YandexScheduleApiUrl
                .Replace("{apiKey}", apiKey)
                .Replace("{date}", currentDate)
                .Replace("{event}", "departure");
            _arrivalFlights = StringConstants.YandexScheduleApiUrl
                .Replace("{apiKey}", apiKey)
                .Replace("{date}", currentDate)
                .Replace("{event}", "arrival");

            Log.Information("Майнер Яндекс.Расписания успешно инициализирован");
        }

        /// <summary>
        /// Получить список рейсов
        /// </summary>
        /// <returns></returns>
        public List<YandexFlight> GetFlights(DirectionType directionType)
        {
            var url = directionType == DirectionType.Departure
                ? _departureFlights
                : _arrivalFlights;
            var response = sendRequest(url);
            return processResponse(response);
        }

        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="requestUrl">URL к API Яндекса</param>
        /// <returns></returns>
        private string sendRequest(string requestUrl)
        {
            var outcome = "";
            var request = WebRequest.Create(requestUrl);

            using (var response = request.GetResponse())
            {
                Log.Information("Ответ успешно получен");

                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return string.Empty;

                    using (var reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            outcome += line;
                    }
                }
            }

            Log.Information("Ответ успешно прочитан");
            return outcome;
        }

        /// <summary>
        /// Парсинг ответа 
        /// </summary>
        /// <param name="responseString">Ответ в виде строки</param>
        /// <returns></returns>
        private List<YandexFlight> processResponse(string responseString)
        {
            Log.Information("Десериализация ответа");

            var response = JsonConvert.DeserializeObject<YandexResponse>(responseString);
            if (response?.Schedules == null)
            {
                Log.Error("Не удалось распарсить ответ Яндекс.Расписания");
                return null;
            }

            var outcome = (from schedule in response.Schedules
                    where schedule.YandexThread != null
                    select new YandexFlight
                    {
                        FlightNumber = schedule.YandexThread.FlightNumber,
                        PlaneName = schedule.YandexThread.Vehicle
                    })
                .ToList();

            Log.Information("Ответ успешно десериализован");
            return outcome;
        }
    }
}
