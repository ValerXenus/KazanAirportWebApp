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
    public class PassengersController : ApiController
    {
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
                using var db = new KazanAirportDbContext();
                var passengersList = db.Passengers
                    .Join(db.Users,
                    p => p.Id,
                    u => u.PassengerId,
                    (p, u) => new PassengerItem
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            MiddleName = p.MiddleName,
                            PassportNumber = p.PassportNumber,
                            UserId = u.Id,
                            UserLogin = u.UserLogin,
                            Email = u.Email
                        })
                    .ToList();

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
        public PassengerItem GetPassengerByPassport(string passportNumber)
        {
            try
            {
                return DbHelpers.GetPassengerByPassport(passportNumber);
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
                using var db = new KazanAirportDbContext();
                var passengerItem = db.Passengers
                    .Where(x => x.Id == passengerId)
                    .Join(db.Users,
                        p => p.Id,
                        u => u.PassengerId,
                        (p, u) => new PassengerItem
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            MiddleName = p.MiddleName,
                            PassportNumber = p.PassportNumber,
                            UserId = u.Id,
                            UserLogin = u.UserLogin,
                            Email = u.Email
                        })
                    .FirstOrDefault();

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
            var existingDataValidation = ValidationLogic.ValidatePassengerData(passenger);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                using var db = new KazanAirportDbContext();
                db.Passengers.Add(passenger);
                db.SaveChanges();

                if (string.IsNullOrEmpty(userLogin))
                    return "Success";

                var addPassengerResult = DbHelpers.AddPassengerToUser(passenger.PassportNumber, userLogin);
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
                using var db = new KazanAirportDbContext();
                var passenger = db.Passengers.FirstOrDefault(x => x.Id == passengerId);
                if (passenger == null)
                    return $"Passenger with ID = {passengerId} wasn't found";

                db.Passengers.Remove(passenger);
                db.SaveChanges();

                // Если пассажир был прикреплен к некоторому пользователю, то открепляем
                var referencedUser = db.Users.FirstOrDefault(x => x.PassengerId == passengerId);
                if (referencedUser == null)
                    return "Success";

                referencedUser.PassengerId = null;
                db.SaveChanges();

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
                using var db = new KazanAirportDbContext();
                var currentPassenger = db.Passengers.FirstOrDefault(x => x.Id == passenger.Id);
                if (currentPassenger == null)
                    return $"Пассажир с ID = {passenger.Id} не был найден";

                currentPassenger.FirstName = passenger.FirstName;
                currentPassenger.LastName = passenger.LastName;
                currentPassenger.MiddleName = passenger.MiddleName;
                currentPassenger.PassportNumber = passenger.PassportNumber;
                db.SaveChanges();

                return "Success";
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }
    }
}