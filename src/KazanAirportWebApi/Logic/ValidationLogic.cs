using System;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Models;

namespace KazanAirportWebApi.Logic
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
        /// <param name="db">Контекст БД</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string ValidateExistingUserData(KazanAirportDbContext db, DbUser user)
        {
            var outcome = "";
            outcome += validateExistingEmail(db, user.Email);
            outcome += validateExistingLogin(db, user.UserLogin);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующий логин в БД, сравнивая с введенным
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="dbUser"></param>
        /// <param name="receivedUser"></param>
        /// <returns></returns>
        public static string ValidateExistingUserForEdit(KazanAirportDbContext db, DbUser dbUser, DbUser receivedUser)
        {
            var outcome = "";

            if (dbUser.UserLogin != receivedUser.UserLogin)
                outcome += validateExistingLogin(db, receivedUser.UserLogin);

            if (dbUser.Email != receivedUser.Email)
                outcome += validateExistingEmail(db, receivedUser.Email);

            return outcome;
        }

        /// <summary>
        /// Валидация полей, что нет пассажиров с такими же данными
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="passenger"></param>
        /// <returns></returns>
        public static string ValidatePassengerData(KazanAirportDbContext db, DbPassenger passenger)
        {
            var outcome = "";

            outcome += validateExistingPassport(db, passenger.PassportNumber);

            return outcome;
        }

        /// <summary>
        /// Проверка полей, что в БД больше нет таких же ICAO и IATA кодов
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="cityData"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportCodes(KazanAirportDbContext db, DbCity cityData)
        {
            var outcome = "";
            outcome += validateExistingIcaoCode(db, cityData.IcaoCode);
            outcome += validateExistingIataCode(db, cityData.IataCode);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующие коды в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="dbCity"></param>
        /// <param name="receivedCity"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportDataForEdit(KazanAirportDbContext db, DbCity dbCity, DbCity receivedCity)
        {
            var outcome = "";

            if (dbCity.IcaoCode != receivedCity.IcaoCode)
                outcome += validateExistingIcaoCode(db, receivedCity.IcaoCode);

            if (dbCity.IataCode != receivedCity.IataCode)
                outcome += validateExistingEmail(db, receivedCity.IataCode);

            return outcome;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Проверка на существование логина в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="login"></param>
        /// <returns></returns>
        private static string validateExistingLogin(KazanAirportDbContext db, string login)
        {
            return checkExistanceInDb(db, login,
                (context, paramValue) => context.Users.Any(x => x.UserLogin == paramValue),
                "- Пользователь с таким логином уже зарегистрирован в системе\n");
        }

        /// <summary>
        /// Проверка на существование Email в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string validateExistingEmail(KazanAirportDbContext db, string email)
        {
            return checkExistanceInDb(db, email,
                (context, paramValue) => context.Users.Any(x => x.Email == paramValue),
                "- Пользователь с таким Email уже присутствует в системе\n");
        }

        /// <summary>
        /// Проверка на существование данного номера паспорта в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        private static string validateExistingPassport(KazanAirportDbContext db, string passportNumber)
        {
            return checkExistanceInDb(db, passportNumber,
                (context, paramValue) => context.Passengers.Any(x => x.PassportNumber == paramValue),
                "- Пассажир с таким номером паспорта уже присутствует в системе\n");
        }

        /// <summary>
        /// Проверка на существование данного ICAO кода в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="icao"></param>
        /// <returns></returns>
        private static string validateExistingIcaoCode(KazanAirportDbContext db, string icao)
        {
            return checkExistanceInDb(db, icao,
                (context, paramValue) => context.Cities.Any(x => x.IcaoCode == paramValue),
                "Аэропорт с таким ICAO уже присутствует в БД");
        }

        /// <summary>
        /// Проверка на существование данного IATA кода в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="iata"></param>
        /// <returns></returns>
        private static string validateExistingIataCode(KazanAirportDbContext db, string iata)
        {
            return checkExistanceInDb(db, iata,
                (context, paramValue) => context.Cities.Any(x => x.IataCode == paramValue),
                "Аэропорт с таким IATA уже присутствует в БД");
        }

        /// <summary>
        /// Проверить на существование некоторого значения в БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        /// <param name="paramValue">Проверяемое значение</param>
        /// <param name="condition">Условие проверки</param>
        /// <param name="errorText">Текст ошибки</param>
        /// <returns></returns>
        private static string checkExistanceInDb(KazanAirportDbContext db, string paramValue, Func<KazanAirportDbContext, string, bool> condition, string errorText)
        {
            var outcome = string.Empty;

            if (condition(db, paramValue))
                outcome += errorText;

            return outcome;
        }

        #endregion
    }
}