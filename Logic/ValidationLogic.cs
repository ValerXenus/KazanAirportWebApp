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
        /// <summary>
        /// Валидация значений, что нет пользователей с такими же данными
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
        /// Проверка, на валидацию существующего логина, сравнивая с актуальным
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
    }
}