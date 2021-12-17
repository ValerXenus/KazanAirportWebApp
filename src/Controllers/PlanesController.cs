using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models;
using KazanAirportWebApp.Models.JoinModels;

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
                using var db = new KazanAirportDbContext();
                var planes = db.Planes.Join(db.Airlines, 
                    p => p.AirlineId, 
                    a => a.Id, 
                    (p, a) => new PlaneItem
                {
                    Id = p.Id,
                    Number = p.Number,
                    Name = p.Name,
                    SeatsNumber = p.SeatsNumber,
                    AirlineId = p.AirlineId,
                    AirlineName = a.Name
                }).ToList();
                return planes;
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
        public DbPlane GetPlaneById(int planeId)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var plane = db.Planes.FirstOrDefault(x => x.Id == planeId);
                return plane;
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
        public string AddNewPlane(DbPlane plane)
        {
            var validationResult = ValidationLogic.ValidateExistingBoardNumbers(plane);
            if (!string.IsNullOrEmpty(validationResult))
                return validationResult;

            try
            {
                using var db = new KazanAirportDbContext();
                db.Planes.Add(plane);
                db.SaveChanges();

                return $"Plane {plane.Name} added successfully.";
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
        public string UpdatePlane(DbPlane plane)
        {
            var dbPlaneItem = GetPlaneById(plane.Id);
            var validationResult = ValidationLogic.ValidateExistingBoardNumbersForEdit(dbPlaneItem, plane);
            if (!string.IsNullOrEmpty(validationResult))
                return validationResult;

            try
            {
                using var db = new KazanAirportDbContext();
                var currentPlane = db.Planes.FirstOrDefault(x => x.Id == plane.Id);
                if (currentPlane == null)
                    return $"Plane with ID = {plane.Id} wasn't found.";

                currentPlane.Name = plane.Name;
                currentPlane.AirlineId = plane.AirlineId;
                currentPlane.Number = plane.Number;
                currentPlane.SeatsNumber = plane.SeatsNumber;
                db.SaveChanges();

                return $"Plane {plane.Name} updated successfully.";
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
                using var db = new KazanAirportDbContext();
                var currentPlane = db.Planes.FirstOrDefault(x => x.Id == planeId);
                if (currentPlane == null)
                    return $"Plane with ID = {planeId} wasn't found.";

                db.Planes.Remove(currentPlane);
                db.SaveChanges();

                return $"Plane {currentPlane.Name} removed successfully.";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}