using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CitiesController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public CitiesController(KazanAirportDbContext db)
        {
            _db = db;
        }

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
                var citiesList = _db.Cities.ToList();
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
                var citiesList = _db.Cities.Where(x => x.Id == cityId).ToList();
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
            var existingDataValidation = ValidationLogic.ValidateExistingAirportCodes(_db, cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                _db.Cities.Add(cityData);
                _db.SaveChanges();
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

            var existingDataValidation = ValidationLogic.ValidateExistingAirportDataForEdit(_db, dbCity, cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                var currentCity = _db.Cities.FirstOrDefault(x => x.Id == cityData.Id);
                if (currentCity == null)
                    return $"City with ID = {cityData.Id} wasn't found.";

                currentCity.Name = cityData.Name;
                currentCity.IcaoCode = cityData.IcaoCode;
                currentCity.IataCode = cityData.IataCode;

                _db.SaveChanges();
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
                var currentCity = _db.Cities.FirstOrDefault(x => x.Id == cityId);
                if (currentCity == null)
                    return $"City with ID = {cityId} wasn't found.";

                _db.Cities.Remove(currentCity);
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