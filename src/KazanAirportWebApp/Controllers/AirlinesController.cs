using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Models;

namespace KazanAirportWebApp.Controllers
{
    public class AirlinesController : ApiController
    {
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
                using var db = new KazanAirportDbContext();
                var airlinesList = db.Airlines.ToList();

                return airlinesList;
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
                using var db = new KazanAirportDbContext();
                var airlinesList = db.Airlines.Where(x => x.Id == airlineId).ToList();

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
                using var db = new KazanAirportDbContext();
                db.Airlines.Add(airlineData);
                db.SaveChanges();

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
                using var db = new KazanAirportDbContext();
                var currentAirline = db.Airlines.FirstOrDefault(x => x.Id == airlineData.Id);
                if (currentAirline == null)
                    return $"Airline with ID = {airlineData.Id} wasn't found";

                currentAirline.Name = airlineData.Name;
                db.SaveChanges();

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
                using var db = new KazanAirportDbContext();
                var currentAirline = db.Airlines.FirstOrDefault(x => x.Id == airlineId);
                if (currentAirline == null)
                    return $"Airline with ID = {airlineId} wasn't found";

                db.Airlines.Remove(currentAirline);
                db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}