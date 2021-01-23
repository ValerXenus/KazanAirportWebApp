using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

namespace KazanAirportWebApp.Controllers
{
    public class PlanesController : ApiController
    {
        /// <summary>
        /// Получение списка самолетов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetPlanesList")]
        public List<PlaneItem> GetPlanesList()
        {
            try
            {
                List<PlaneItem> planeItems;
                using (var db = new KazanAirportDbEntities())
                {
                    planeItems = db.Database
                        .SqlQuery<PlaneItem>("Select * From dbo.Planes as P Join dbo.Airlines as A on " +
                                             "P.airlineId = A.id").ToList();
                }

                return planeItems;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение самолета по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetPlaneById")]
        public PlaneItem GetPlaneById(int planeId)
        {
            try
            {
                List<PlaneItem> planeItems;
                using (var db = new KazanAirportDbEntities())
                {
                    planeItems = db.Database.SqlQuery<PlaneItem>("Select * From dbo.Planes as P Join dbo.Airlines as A " +
                                                                 "on P.airlineId = A.id Where P.id = @id",
                        new SqlParameter("@id", planeId)).ToList();
                }

                return planeItems.Count == 0 ? null : planeItems.First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить самолет
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewPlane")]
        public string AddNewPlane(PlaneItem planeItem)
        {
            var validationResult = ValidationLogic.ValidateExistingBoardNumbers(planeItem);
            if (!string.IsNullOrEmpty(validationResult))
                return validationResult;

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Planes (modelName, boardNumber, seatsNumber, airlineId) " +
                        "Values (@modelName, @boardNumber, @seatsNumber, @airlineId)",
                        new SqlParameter("@modelName", planeItem.modelName),
                        new SqlParameter("@boardNumber", planeItem.boardNumber),
                        new SqlParameter("@seatsNumber", planeItem.seatsNumber),
                        new SqlParameter("@airlineId", planeItem.airlineId));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Обновить данные самолета
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdatePlane")]
        public string UpdatePlane(PlaneItem planeItem)
        {
            var dbPlaneItem = GetPlaneById(planeItem.id);
            var validationResult = ValidationLogic.ValidateExistingBoardNumbersForEdit(dbPlaneItem, planeItem);
            if (!string.IsNullOrEmpty(validationResult))
                return validationResult;

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Update dbo.Planes Set modelName = @modelName, boardNumber = @boardNumber, " +
                        "seatsNumber = @seatsNumber, airlineId = @airlineId",
                        new SqlParameter("@modelName", planeItem.modelName),
                        new SqlParameter("@boardNumber", planeItem.boardNumber),
                        new SqlParameter("@seatsNumber", planeItem.seatsNumber),
                        new SqlParameter("@airlineId", planeItem.airlineId));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Удалить самолет
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemovePlane")]
        public string RemovePlane(int planeId)
        {
            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Planes where id = @id",
                        new SqlParameter("@id", planeId));
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