using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using KazanAirportWebApi.Models.JoinModels;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PassengersController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public PassengersController(KazanAirportDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение списка пассажиров
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetPassengersList")]
        public List<PassengerItem> GetPassengersList()
        {
            try
            {
                var passengersList = (from p in _db.Passengers
                    join u in _db.Users on p.Id equals u.PassengerId into du
                    from subUser in du.DefaultIfEmpty()
                    select new PassengerItem
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        MiddleName = p.MiddleName,
                        PassportNumber = p.PassportNumber,
                        UserId = (subUser == null ? 0 : subUser.Id),
                        UserLogin = (subUser == null ? string.Empty : subUser.UserLogin),
                        Email = (subUser == null ? string.Empty : subUser.Email)
                    }).ToList();

                return passengersList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение пассажира по номеру паспорта
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetPassengerByPassport")]
        public DbPassenger GetPassengerByPassport(string passportNumber)
        {
            try
            {
                return DbHelpers.GetPassengerByPassport(_db, passportNumber);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение пассажира по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetPassengerById")]
        public PassengerItem GetPassengerById(int passengerId)
        {
            try
            {
                var passengerItem = (from p in _db.Passengers
                    where p.Id == passengerId
                    join u in _db.Users on p.Id equals u.PassengerId into du
                    from subUser in du.DefaultIfEmpty()
                    select new PassengerItem
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        MiddleName = p.MiddleName,
                        PassportNumber = p.PassportNumber,
                        UserId = (subUser == null ? 0 : subUser.Id),
                        UserLogin = (subUser == null ? string.Empty : subUser.UserLogin),
                        Email = (subUser == null ? string.Empty : subUser.Email)
                    }).FirstOrDefault();

                return passengerItem;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Добавить нового пассажира
        /// </summary>
        /// <param name="passenger">Добавляемый пассажир</param>
        /// <param name="userLogin">
        /// Логин пользователя, к которому необходимо добавить данные пассажира.
        /// "" - если добавлять не нужно
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewPassenger")]
        public string AddNewPassenger(DbPassenger passenger, string userLogin = "")
        {
            var existingDataValidation = ValidationLogic.ValidatePassengerData(_db, passenger);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                _db.Passengers.Add(passenger);
                _db.SaveChanges();

                if (string.IsNullOrEmpty(userLogin))
                    return "Success";

                var addPassengerResult = DbHelpers.AddPassengerToUser(_db, passenger.PassportNumber, userLogin);
                return !addPassengerResult.Equals("Success") 
                    ? addPassengerResult 
                    : "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Удаление пассажира
        /// </summary>
        /// <param name="passengerId"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemovePassenger")]
        public string RemovePassenger(int passengerId)
        {
            try
            {
                var passenger = _db.Passengers.FirstOrDefault(x => x.Id == passengerId);
                if (passenger == null)
                    return $"Passenger with ID = {passengerId} wasn't found";

                _db.Passengers.Remove(passenger);
                _db.SaveChanges();

                // Если пассажир был прикреплен к некоторому пользователю, то открепляем
                var referencedUser = _db.Users.FirstOrDefault(x => x.PassengerId == passengerId);
                if (referencedUser == null)
                    return "Success";

                referencedUser.PassengerId = null;
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Обновление данных пассажира
        /// </summary>
        /// <param name="passenger">Добавляемый пассажир</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdatePassenger")]
        public string UpdatePassenger(PassengerItem passenger)
        {
            try
            {
                var currentPassenger = _db.Passengers.FirstOrDefault(x => x.Id == passenger.Id);
                if (currentPassenger == null)
                    return $"Пассажир с ID = {passenger.Id} не был найден";

                currentPassenger.FirstName = passenger.FirstName;
                currentPassenger.LastName = passenger.LastName;
                currentPassenger.MiddleName = passenger.MiddleName;
                currentPassenger.PassportNumber = passenger.PassportNumber;
                _db.SaveChanges();

                return "Success";
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }
    }
}