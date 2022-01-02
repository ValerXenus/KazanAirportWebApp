using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Models;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PlanesController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public PlanesController(KazanAirportDbContext db)
        {
            _db = db;
        }

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
                var planes = _db.Planes.Join(_db.Airlines, 
                    p => p.AirlineId, 
                    a => a.Id, 
                    (p, a) => new PlaneItem
                {
                    Id = p.Id,
                    Name = p.Name,
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
                var plane = _db.Planes.FirstOrDefault(x => x.Id == planeId);
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
            try
            {
                _db.Planes.Add(plane);
                _db.SaveChanges();

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
        public string UpdatePlane(DbPlane plane)
        {
            try
            {
                var currentPlane = _db.Planes.FirstOrDefault(x => x.Id == plane.Id);
                if (currentPlane == null)
                    return $"Plane with ID = {plane.Id} wasn't found.";

                currentPlane.Name = plane.Name;
                currentPlane.AirlineId = plane.AirlineId;
                _db.SaveChanges();

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
                var currentPlane = _db.Planes.FirstOrDefault(x => x.Id == planeId);
                if (currentPlane == null)
                    return $"Plane with ID = {planeId} wasn't found.";

                _db.Planes.Remove(currentPlane);
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