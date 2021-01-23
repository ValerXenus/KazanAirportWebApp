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
                List<PassengerItem> passengersList;
                using (var db = new KazanAirportDbEntities())
                {
                    passengersList = db.Database.
                        SqlQuery<PassengerItem>("Select * From dbo.Passengers as P " +
                                                "Left Join dbo.Logins as L On P.id = L.passengerId").ToList();
                }

                return passengersList;
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
                List<PassengerItem> passengersList;
                using (var db = new KazanAirportDbEntities())
                {
                    passengersList = db.Database.
                        SqlQuery<PassengerItem>("Select * From dbo.Passengers as P " +
                                                "Left Join dbo.Logins as L On P.id = L.passengerId " +
                                                "Where P.id = @passengerId",
                            new SqlParameter("@passengerId", passengerId)).ToList();

                    if (passengersList.Count == 0)
                        return null;

                }

                return passengersList.First();
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
        public string AddNewPassenger(Passengers passenger, string userLogin = "")
        {
            var existingDataValidation = ValidationLogic.ValidatePassengerData(passenger);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Passengers(lastName, firstName, middleName, passportNumber) " +
                        "Values (@lastName, @firstName, @middleName, @passportNumber)",
                        new SqlParameter("@lastName", passenger.lastName),
                        new SqlParameter("@firstName", passenger.firstName),
                        new SqlParameter("@middleName", passenger.middleName),
                        new SqlParameter("@passportNumber", passenger.passportNumber));
                }

                if (!string.IsNullOrEmpty(userLogin))
                {
                    var addPassengerResult = DbHelpers.AddPassengerToUser(passenger.passportNumber, userLogin);
                    if (!addPassengerResult.Equals("Success"))
                    {
                        return addPassengerResult;
                    }
                }

                return "Success";
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
                var userId = DbHelpers.GetUserId(passengerId);

                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Passengers where id = @passengerId",
                        new SqlParameter("@passengerId", passengerId));

                    // Если пассажир был прикреплен к некоторому пользователю, то открепляем
                    if (userId != -1)
                    {
                        db.Database
                            .ExecuteSqlCommand("Update dbo.Logins Set passengerId = -1 Where id = @userId",
                                new SqlParameter("@userId", userId));
                    }
                }

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
            var dbPassenger = GetPassengerById(passenger.id);
            if (dbPassenger == null)
            {
                return "Ошибка. Пассажир не найден";
            }

            var existingDataValidation = ValidationLogic.ValidatePassengerDataForEdit(dbPassenger, passenger);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            var userId = DbHelpers.GetUserId(passenger.id, passenger.login);

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database
                        .ExecuteSqlCommand("Update dbo.Passengers Set lastName = @lastName, firstName = @firstName, middleName = @middleName, " +
                                           "passportNumber = @passportNumber Where id = @id",
                            new SqlParameter("@lastName", passenger.lastName),
                            new SqlParameter("@firstName", passenger.firstName),
                            new SqlParameter("@middleName", passenger.middleName),
                            new SqlParameter("@passportNumber", passenger.passportNumber),
                            new SqlParameter("@id", passenger.id));

                    if (userId == -1)
                        return "Success";

                    int passengerId = passenger.id;

                    // Удаляем пассажира от пользователя, если поле логина стало пустым
                    if (string.IsNullOrEmpty(passenger.login))
                        passengerId = -1;

                    db.Database
                        .ExecuteSqlCommand("Update dbo.Logins Set passengerId = @passengerId Where id = @id",
                            new SqlParameter("@passengerId", passengerId),
                            new SqlParameter("@id", userId));
                }

                return "Success";
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }
    }
}