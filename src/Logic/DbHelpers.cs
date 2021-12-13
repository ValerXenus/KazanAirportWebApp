using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

namespace KazanAirportWebApp.Logic
{
    /// <summary>
    /// Класс дополнительных запросов к БД
    /// </summary>
    public class DbHelpers
    {
        /// <summary>
        /// Добавление информации о пассажире к пользователю
        /// </summary>
        /// <param name="passengerPassport"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public static string AddPassengerToUser(string passengerPassport, string userLogin)
        {
            var outcome = "";

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    // Получение пассажира по номеру паспорта
                    var passengers = db.Database
                        .SqlQuery<Passengers>("Select * From dbo.Passengers Where passportNumber = @passportNumber",
                            new SqlParameter("@passportNumber", passengerPassport)).ToList();
                    if (passengers.Count == 0)
                        return "Пассажир с таким номером паспорта не найден";

                    // Проверка, что указанный индекс пассажира еще не прицеплен ни к одному пользователю
                    var userWithPassengerId = db.Database
                        .SqlQuery<Logins>("Select * From dbo.Logins Where passengerId = @passengerId",
                            new SqlParameter("@passengerId", passengers.First().id)).ToList();
                    if (userWithPassengerId.Count > 0)
                        return "Указанный пассажир уже добавлен к пользователю";

                    // Проверка, что на указанного пользователя еще не повешен id какого-либо пассажира
                    var foundUsers = db.Database
                        .SqlQuery<Logins>("Select * From dbo.Logins Where [login] = @userLogin",
                            new SqlParameter("@userLogin", userLogin)).ToList();
                    if (foundUsers.Count == 0)
                        return $"Пользователь с логином: {userLogin} не найден";
                    if (foundUsers.First().passengerId != null)
                        return $"У пользователя с ID: {foundUsers.First().id} уже указан другой пассажир";

                    db.Database
                        .ExecuteSqlCommand("Update dbo.Logins Set passengerId = @passengerId Where id = @id",
                            new SqlParameter("@passengerId", passengers.First().id),
                            new SqlParameter("@id", foundUsers.First().id));
                    outcome = "Success";
                }
            }
            catch (Exception exception)
            {
                outcome = exception.Message;
            }

            return outcome;
        }

        /// <summary>
        /// Получение Id пользователя по id пассажира или логину
        /// </summary>
        /// <returns></returns>
        public static int GetUserId(int passengerId, string userLogin = "")
        {
            using (var db = new KazanAirportDbEntities())
            {
                List<Logins> userLogins;

                if (!string.IsNullOrEmpty(userLogin))
                {
                    userLogins = db.Database
                        .SqlQuery<Logins>("Select * From dbo.Logins Where login = @userLogin",
                            new SqlParameter("@userLogin", userLogin)).ToList();

                    if (userLogins.Count > 0)
                        return userLogins.First().id;
                }

                userLogins = db.Database
                    .SqlQuery<Logins>("Select * From dbo.Logins Where passengerId = @passengerId",
                        new SqlParameter("@passengerId", passengerId)).ToList();
                if (userLogins.Count > 0)
                    return userLogins.First().id;
            }

            return -1;
        }

        /// <summary>
        /// Получение пассажира по номеру паспорта
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        public static PassengerItem GetPassengerByPassport(string passportNumber)
        {
            List<PassengerItem> passengersList;
            using (var db = new KazanAirportDbEntities())
            {
                passengersList = db.Database.
                    SqlQuery<PassengerItem>("Select * From dbo.Passengers as P " +
                                            "Left Join dbo.Logins as L On P.id = L.passengerId " +
                                            "Where P.passportNumber = @passportNumber",
                        new SqlParameter("@passportNumber", passportNumber)).ToList();

                if (passengersList.Count == 0)
                    return null;

            }

            return passengersList.First();
        }
    }
}