using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AirlinesController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly IConfiguration _config;

        public AirlinesController(IConfiguration config, KazanAirportDbContext db)
        {
            _db = db;
            _config = config;
        }

        /// <summary>
        /// Получение списка авиакомпаний
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetAirlinesList")]
        public List<DbAirline> GetAirlinesList()
        {
            try
            {
                var airlinesList = _db.Airlines.ToList();

                return airlinesList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение списка рейтинга авиакомпаний
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetAirlinesRating")]
        public List<AirlineTopItem> GetAirlinesRating()
        {
            try
            {
                return FlightsFileReader.Instance(_config).GetFlightsRating();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение авиакомпании по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetAirlineById")]
        public DbAirline GetAirlineById(int airlineId)
        {
            try
            {
                var airlinesList = _db.Airlines.Where(x => x.Id == airlineId).ToList();

                return airlinesList.Count == 0 
                    ? null 
                    : airlinesList.First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить авиакомпанию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewAirline")]
        public string AddNewAirline(DbAirline airlineData)
        {
            try
            {
                _db.Airlines.Add(airlineData);
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Обновить данные авиакомпании
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAirline")]
        public string UpdateAirline(DbAirline airlineData)
        {
            try
            {
                var currentAirline = _db.Airlines.FirstOrDefault(x => x.Id == airlineData.Id);
                if (currentAirline == null)
                    return $"Airline with ID = {airlineData.Id} wasn't found";

                currentAirline.Name = airlineData.Name;
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Удалить авиакомпанию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemoveAirline")]
        public string RemoveAirline(int airlineId)
        {
            try
            {
                var currentAirline = _db.Airlines.FirstOrDefault(x => x.Id == airlineId);
                if (currentAirline == null)
                    return $"Airline with ID = {airlineId} wasn't found";

                _db.Airlines.Remove(currentAirline);
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}