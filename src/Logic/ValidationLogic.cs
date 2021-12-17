using System;
using System.Linq;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Models;
using KazanAirportWebApp.Models.Join_Models;

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
        public static string ValidateExistingUserData(DbUser user)
        {
            var outcome = "";
            outcome += validateExistingEmail(user.Email);
            outcome += validateExistingLogin(user.UserLogin);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующий логин в БД, сравнивая с введенным
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="receivedUser"></param>
        /// <returns></returns>
        public static string ValidateExistingUserForEdit(DbUser dbUser, DbUser receivedUser)
        {
            var outcome = "";

            if (dbUser.UserLogin != receivedUser.UserLogin)
                outcome += validateExistingLogin(receivedUser.UserLogin);

            if (dbUser.Email != receivedUser.Email)
                outcome += validateExistingEmail(receivedUser.Email);

            return outcome;
        }

        /// <summary>
        /// Валидация полей, что нет пассажиров с такими же данными
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        public static string ValidatePassengerData(DbPassenger passenger)
        {
            var outcome = "";

            outcome += validateExistingPassport(passenger.PassportNumber);

            return outcome;
        }

        /// <summary>
        /// Проверка номера паспорта в БД, сравнивая с введенным
        /// </summary>
        /// <param name="dbPassenger"></param>
        /// <param name="receivedPassenger"></param>
        /// <returns></returns>
        public static string ValidatePassengerDataForEdit(DbPassenger dbPassenger, DbPassenger receivedPassenger)
        {
            var outcome = "";

            if (dbPassenger.PassportNumber != receivedPassenger.PassportNumber)
                outcome += validateExistingPassport(receivedPassenger.PassportNumber);

            return outcome;
        }

        /// <summary>
        /// Проверка полей, что в БД больше нет таких же ICAO и IATA кодов
        /// </summary>
        /// <param name="cityData"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportCodes(DbCity cityData)
        {
            var outcome = "";
            outcome += validateExistingIcaoCode(cityData.IcaoCode);
            outcome += validateExistingIataCode(cityData.IataCode);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующие коды в БД
        /// </summary>
        /// <param name="dbCity"></param>
        /// <param name="receivedCity"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportDataForEdit(DbCity dbCity, DbCity receivedCity)
        {
            var outcome = "";

            if (dbCity.IcaoCode != receivedCity.IcaoCode)
                outcome += validateExistingIcaoCode(receivedCity.IcaoCode);

            if (dbCity.IataCode != receivedCity.IataCode)
                outcome += validateExistingEmail(receivedCity.IataCode);

            return outcome;
        }

        /// <summary>
        /// Проверка, что в БД больше нет самолета с таким же бортовым номером
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public static string ValidateExistingBoardNumbers(DbPlane plane)
        {
            var outcome = "";
            outcome += validateExistingBoardNumber(plane.Number);

            return outcome;
        }

        /// <summary>
        /// Проверка, что в БД больше нет самолета с таким же бортовым номером, для редактирования
        /// </summary>
        /// <param name="dbPlane"></param>
        /// <param name="receivedPlane"></param>
        /// <returns></returns>
        public static string ValidateExistingBoardNumbersForEdit(DbPlane dbPlane, DbPlane receivedPlane)
        {
            var outcome = "";
            if (dbPlane == null || receivedPlane == null)
                return outcome;

            if (dbPlane.Number != receivedPlane.Number)
                outcome += validateExistingBoardNumber(receivedPlane.Number);

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
            return checkExistanceInDb(login,
                (db, paramValue) => db.Users.Any(x => x.UserLogin == paramValue),
                "- Пользователь с таким логином уже зарегистрирован в системе\n");
        }

        /// <summary>
        /// Проверка на существование Email в БД
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static string validateExistingEmail(string email)
        {
            return checkExistanceInDb(email,
                (db, paramValue) => db.Users.Any(x => x.Email == paramValue),
                "- Пользователь с таким Email уже присутствует в системе\n");
        }

        /// <summary>
        /// Проверка на существование данного номера паспорта в БД
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        private static string validateExistingPassport(string passportNumber)
        {
            return checkExistanceInDb(passportNumber,
                (db, paramValue) => db.Passengers.Any(x => x.PassportNumber == paramValue),
                "- Пассажир с таким номером паспорта уже присутствует в системе\n");
        }

        /// <summary>
        /// Проверка на существование данного ICAO кода в БД
        /// </summary>
        /// <param name="icao"></param>
        /// <returns></returns>
        private static string validateExistingIcaoCode(string icao)
        {
            return checkExistanceInDb(icao,
                (db, paramValue) => db.Cities.Any(x => x.IcaoCode == paramValue),
                "Аэропорт с таким ICAO уже присутствует в БД");
        }

        /// <summary>
        /// Проверка на существование данного IATA кода в БД
        /// </summary>
        /// <param name="iata"></param>
        /// <returns></returns>
        private static string validateExistingIataCode(string iata)
        {
            return checkExistanceInDb(iata,
                (db, paramValue) => db.Cities.Any(x => x.IataCode == paramValue),
                "Аэропорт с таким IATA уже присутствует в БД");
        }

        /// <summary>
        /// Проверка на существование данного бортового номера в БД
        /// </summary>
        /// <param name="boardNumber"></param>
        /// <returns></returns>
        private static string validateExistingBoardNumber(string boardNumber)
        {
            return checkExistanceInDb(boardNumber, 
                (db, paramValue) => db.Planes.Any(x => x.Number == paramValue),
                "Самолет с данным бортовым номером уже присутствует в БД");
        }

        /// <summary>
        /// Проверить на существование некоторого значения в БД
        /// </summary>
        /// <param name="paramValue">Проверяемое значение</param>
        /// <param name="condition">Условие проверки</param>
        /// <param name="errorText">Текст ошибки</param>
        /// <returns></returns>
        private static string checkExistanceInDb(string paramValue, Func<KazanAirportDbContext, string, bool> condition, string errorText)
        {
            var outcome = string.Empty;

            using var db = new KazanAirportDbContext();
            if (condition(db, paramValue))
                outcome += errorText;

            return outcome;
        }

        #endregion
    }
}