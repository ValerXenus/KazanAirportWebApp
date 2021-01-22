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
        public string AddNewUser(Passengers passenger, string userLogin = "")
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
    }
}