using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models;

namespace KazanAirportWebApp.Controllers
{
    public class CitiesController : ApiController
    {
        /// <summary>
        /// Получение списка городов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetCitiesList")]
        public List<DbCity> GetCitiesList()
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var citiesList = db.Cities.ToList();
                return citiesList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение города по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetCityById")]
        public DbCity GetCityById(int cityId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var citiesList = db.Cities.Where(x => x.Id == cityId).ToList();
                return citiesList.Count == 0 ? null : citiesList.First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить город
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewCity")]
        public string AddNewCity(DbCity cityData)
        {
            var existingDataValidation = ValidationLogic.ValidateExistingAirportCodes(cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                using var db = new KazanAirportDbContext();
                db.Cities.Add(cityData);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Обновить данные города
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateCity")]
        public string UpdateCity(DbCity cityData)
        {
            var dbCity = GetCityById(cityData.Id);

            var existingDataValidation = ValidationLogic.ValidateExistingAirportDataForEdit(dbCity, cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                using var db = new KazanAirportDbContext();
                var currentCity = db.Cities.FirstOrDefault(x => x.Id == cityData.Id);
                if (currentCity == null)
                    return $"City with ID = {cityData.Id} wasn't found.";

                currentCity.Name = cityData.Name;
                currentCity.IcaoCode = cityData.IcaoCode;
                currentCity.IataCode = cityData.IataCode;

                db.SaveChanges();
                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Удалить город
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemoveCity")]
        public string RemoveCity(int cityId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var currentCity = db.Cities.FirstOrDefault(x => x.Id == cityId);
                if (currentCity == null)
                    return $"City with ID = {cityId} wasn't found.";

                db.Cities.Remove(currentCity);
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