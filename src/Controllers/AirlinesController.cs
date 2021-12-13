using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.Models.Data_Access;

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
        public List<Airlines> GetAirlinesList()
        {
            try
            {
                List<Airlines> airlinesList;
                using (var db = new KazanAirportDbEntities())
                {
                    airlinesList = db.Database.
                        SqlQuery<Airlines>("Select * From dbo.Airlines").ToList();
                }

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
        public Airlines GetAirlineById(int airlineId)
        {
            try
            {
                List<Airlines> airlinesList;
                using (var db = new KazanAirportDbEntities())
                {
                    airlinesList = db.Database.SqlQuery<Airlines>("Select * From dbo.Airlines Where id = @id",
                        new SqlParameter("@id", airlineId)).ToList();
                }

                return airlinesList.Count == 0 ? null : airlinesList.First();
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
        public string AddNewAirline(Airlines airlineData)
        {
            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Airlines(airlineName) Values (@airlineName)",
                        new SqlParameter("@airlineName", airlineData.airlineName));
                }

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
        public string UpdateAirline(Airlines airlineData)
        {
            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Update dbo.Airlines set airlineName = @airlineName Where id = @id",
                        new SqlParameter("@airlineName", airlineData.airlineName),
                        new SqlParameter("id", airlineData.id));
                }

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
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Airlines where id = @id",
                        new SqlParameter("@id", airlineId));
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