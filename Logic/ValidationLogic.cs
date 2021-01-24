using System.Data.SqlClient;
using System.Linq;
using KazanAirportWebApp.Models.Data_Access;
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
        public static string ValidateExistingUserForEdit(Logins dbUser, Logins receivedUser)
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

        /// <summary>
        /// Проверка, на существующий логин и номера паспорта в БД, сравнивая с введенным
        /// </summary>
        /// <param name="dbPassenger"></param>
        /// <param name="receivedPassenger"></param>
        /// <returns></returns>
        public static string ValidatePassengerDataForEdit(PassengerItem dbPassenger, PassengerItem receivedPassenger)
        {
            var outcome = "";

            if (dbPassenger.passportNumber != receivedPassenger.passportNumber)
                outcome += validateExistingPassport(receivedPassenger.passportNumber);

            if (dbPassenger.login != null 
                && dbPassenger.login != receivedPassenger.login)
            {
                // Проверяем, что логин ни кем не используется, и обновляем
                using (var db = new KazanAirportDbEntities())
                {
                    var userLogins = db.Database
                        .SqlQuery<Logins>("Select * From dbo.Logins Where [login] = @login",
                            new SqlParameter("@login", receivedPassenger.login)).ToList();

                    if (userLogins.Count > 0
                        && userLogins.First().id != dbPassenger.id)
                    {
                        outcome += "Нельзя добавить данные пассажира к пользователю, у которого уже указаны " +
                                   "другие данные пассажира";
                    }
                }
            }

            return outcome;
        }

        /// <summary>
        /// Проверка полей, что в БД больше нет таких же ICAO и IATA кодов
        /// </summary>
        /// <param name="cityData"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportCodes(Cities cityData)
        {
            var outcome = "";
            outcome += validateExistingIcaoCode(cityData.icaoCode);
            outcome += validateExistingIataCode(cityData.iataCode);

            return outcome;
        }

        /// <summary>
        /// Проверка, на существующие коды в БД
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="receivedUser"></param>
        /// <returns></returns>
        public static string ValidateExistingAirportDataForEdit(Cities dbCity, Cities receivedCity)
        {
            var outcome = "";

            if (dbCity.icaoCode != receivedCity.icaoCode)
                outcome += validateExistingIcaoCode(receivedCity.icaoCode);

            if (dbCity.iataCode != receivedCity.iataCode)
                outcome += validateExistingEmail(receivedCity.iataCode);

            return outcome;
        }

        /// <summary>
        /// Проверка, что в БД больше нет самолета с таким же бортовым номером
        /// </summary>
        /// <param name="planeItem"></param>
        /// <returns></returns>
        public static string ValidateExistingBoardNumbers(PlaneItem planeItem)
        {
            var outcome = "";
            outcome += validateExistingBoardNumber(planeItem.boardNumber);

            return outcome;
        }

        /// <summary>
        /// Проверка, что в БД больше нет самолета с таким же бортовым номером, для редактирования
        /// </summary>
        /// <param name="dbPlaneItem"></param>
        /// <param name="receivedPlaneItem"></param>
        /// <returns></returns>
        public static string ValidateExistingBoardNumbersForEdit(PlaneItem dbPlaneItem, PlaneItem receivedPlaneItem)
        {
            var outcome = "";

            if (dbPlaneItem.boardNumber != receivedPlaneItem.boardNumber)
                outcome += validateExistingBoardNumber(receivedPlaneItem.boardNumber);

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
                if (loginIds.Count > 0)
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
                if (loginIds.Count > 0)
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
                if (loginIds.Count > 0)
                    outcome += "- Пассажир с таким номером паспорта уже присутствует в системе\n";
            }

            return outcome;
        }

        /// <summary>
        /// Проверка на существование данного ICAO кода в БД
        /// </summary>
        /// <param name="icao"></param>
        /// <returns></returns>
        private static string validateExistingIcaoCode(string icao)
        {
            var outcome = string.Empty;

            using (var db = new KazanAirportDbEntities())
            {
                var foundCities = db.Database
                    .SqlQuery<Cities>("Select * From dbo.Cities Where icaoCode = @icaoCode",
                        new SqlParameter("@icaoCode", icao)).ToList();
                if (foundCities.Count > 0)
                    outcome += "Аэропорт с таким ICAO уже присутствует в БД";
            }

            return outcome;
        }

        /// <summary>
        /// Проверка на существование данного IATA кода в БД
        /// </summary>
        /// <param name="iato"></param>
        /// <returns></returns>
        private static string validateExistingIataCode(string iata)
        {
            var outcome = string.Empty;

            using (var db = new KazanAirportDbEntities())
            {
                var foundCities = db.Database
                    .SqlQuery<Cities>("Select * From dbo.Cities Where iataCode = @iataCode",
                        new SqlParameter("@iataCode", iata)).ToList();
                if (foundCities.Count > 0)
                    outcome += "Аэропорт с таким IATA уже присутствует в БД";
            }

            return outcome;
        }

        /// <summary>
        /// Проверка на существование данного бортового номера в БД
        /// </summary>
        /// <param name="boardNumber"></param>
        /// <returns></returns>
        private static string validateExistingBoardNumber(string boardNumber)
        {
            var outcome = string.Empty;

            using (var db = new KazanAirportDbEntities())
            {
                var foundPlanes = db.Database
                    .SqlQuery<Planes>("Select * From dbo.Planes Where boardNumber = @boardNumber",
                        new SqlParameter("@boardNumber", boardNumber)).ToList();
                if (foundPlanes.Count > 0)
                    outcome += "Самолет с данным бортовым номером уже присутствует в БД";
            }

            return outcome;
        }

        #endregion

    }
}