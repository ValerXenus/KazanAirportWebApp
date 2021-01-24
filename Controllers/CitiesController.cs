using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;

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
        public List<Cities> GetCitiesList()
        {
            try
            {
                List<Cities> citiesList;
                using (var db = new KazanAirportDbEntities())
                {
                    citiesList = db.Database.
                        SqlQuery<Cities>("Select * From dbo.Cities").ToList();
                }

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
        public Cities GetCityById(int cityId)
        {
            try
            {
                List<Cities> citiesList;
                using (var db = new KazanAirportDbEntities())
                {
                    citiesList = db.Database.SqlQuery<Cities>("Select * From dbo.Cities Where id = @id",
                        new SqlParameter("@id", cityId)).ToList();
                }

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
        public string AddNewCity(Cities cityData)
        {
            var existingDataValidation = ValidationLogic.ValidateExistingAirportCodes(cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Cities (cityName, icaoCode, iataCode) Values (@cityName, @icaoCode, @iataCode)",
                        new SqlParameter("@cityName", cityData.cityName),
                        new SqlParameter("@icaoCode", cityData.icaoCode),
                        new SqlParameter("@iataCode", cityData.iataCode));
                }

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
        public string UpdateCity(Cities cityData)
        {
            var dbCity = GetCityById(cityData.id);

            var existingDataValidation = ValidationLogic.ValidateExistingAirportDataForEdit(dbCity, cityData);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Update dbo.Cities Set cityName = @cityName, icaoCode = @icaoCode, iataCode = @iataCode Where id = @id",
                        new SqlParameter("@cityName", cityData.cityName),
                        new SqlParameter("@icaoCode", cityData.icaoCode),
                        new SqlParameter("@iataCode", cityData.iataCode),
                        new SqlParameter("id", cityData.id));
                }

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
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Cities where id = @id",
                        new SqlParameter("@id", cityId));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}