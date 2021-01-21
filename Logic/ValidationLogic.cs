using System;
using System.Data.SqlClient;
using System.Linq;
using KazanAirportWebApp.Models.Data_Access;

namespace KazanAirportWebApp.Logic
{
    /// <summary>
    /// Класс логики для валидации значений
    /// </summary>
    public class ValidationLogic
    {
        #region Public methods

        /// <summary>
        /// Валидация полей, что нет пользователей с такими же данными
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string ValidateExistingUserData(Logins user)
        {
            var outcome = "";
            outcome += validateExistingEmail(user.email);
            outcome += validateExistingLogin(user.login);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующий логин в БД, сравнивая с введенным
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="receivedUser"></param>
        /// <returns></returns>
        public static string ValidateExistingUserCompare(Logins dbUser, Logins receivedUser)
        {
            var outcome = "";

            if (dbUser.login != receivedUser.login)
                outcome += validateExistingLogin(receivedUser.login);

            if (dbUser.email != receivedUser.email)
                outcome += validateExistingEmail(receivedUser.email);

            return outcome;
        }

        /// <summary>
        /// Валидация полей, что нет пассажиров с такими же данными
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        public static string ValidatePassengerData(Passengers passenger)
        {
            var outcome = "";

            outcome += validateExistingPassport(passenger.passportNumber);

            return outcome;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Проверка на существование логина в БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private static string validateExistingLogin(string login)
        {
            var outcome = string.Empty;
            using (var db = new KazanAirportDbEntities())
            {
                var loginIds = db.Database.SqlQuery<int>("Select id From dbo.Logins Where login = @login",
                    new SqlParameter("@login", login)).ToList();
                if (loginIds.Count != 0)
                    outcome += "- Пользователь с таким логином уже зарегистрирован в системе\n";
            }

            return outcome;
        }

        /// <summary>
        /// Проверка на существование Email в БД
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string validateExistingEmail(string email)
        {
            var outcome = string.Empty;
            using (var db = new KazanAirportDbEntities())
            {
                var loginIds = db.Database.SqlQuery<int>("Select id From dbo.Logins Where email = @email",
                    new SqlParameter("@email", email)).ToList();
                if (loginIds.Count != 0)
                    outcome += "- Пользователь с таким Email уже присутствует в системе\n";
            }

            return outcome;
        }

        /// <summary>
        /// Проверка на существование данного номера паспорта в БД
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        private static string validateExistingPassport(string passportNumber)
        {
            var outcome = string.Empty;

            using (var db = new KazanAirportDbEntities())
            {
                var loginIds = db.Database
                    .SqlQuery<int>("Select id From dbo.Passengers Where passportNumber = @passportNumber",
                    new SqlParameter("@passportNumber", passportNumber)).ToList();
                if (loginIds.Count != 0)
                    outcome += "- Пассажир с таким номером паспорта уже присутствует в системе\n";
            }

            return outcome;
        }

        #endregion

    }
}