using System;
using System.Linq;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Models;

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
            var outcome = string.Empty;

            try
            {
                using var db = new KazanAirportDbContext();
                // Получение пассажира по номеру паспорта
                var passenger = db.Passengers.FirstOrDefault(x => x.PassportNumber == passengerPassport);
                if (passenger == null)
                    return "Пассажир с таким номером паспорта не найден";

                // Проверка, что указанный индекс пассажира еще не привязан ни к одному пользователю
                var userWithPassengerId = db.Users.FirstOrDefault(x => x.PassengerId == passenger.Id);
                if (userWithPassengerId != null)
                    return "Указанный пассажир уже добавлен к пользователю";

                // Проверка, что на указанного пользователя еще не привязан id какого-либо пассажира
                var currentUser = db.Users.FirstOrDefault(x => x.UserLogin == userLogin);
                if (currentUser == null)
                    return $"Пользователь с логином: {userLogin} не найден";

                if (currentUser.PassengerId != null)
                    return $"К пользователю {currentUser.UserLogin} уже привязан другой пассажир";

                // Привязка пассажира к пользователю
                currentUser.PassengerId = passenger.Id;
                db.SaveChanges();
                outcome = "Success";
            }
            catch (Exception exception)
            {
                outcome = exception.Message;
            }

            return outcome;
        }

        /// <summary>
        /// Получение пассажира по номеру паспорта
        /// </summary>
        /// <param name="passportNumber"></param>
        /// <returns></returns>
        public static DbPassenger GetPassengerByPassport(string passportNumber)
        {
            using var db = new KazanAirportDbContext();
            var passengerItem = db.Passengers
                .FirstOrDefault(x => x.PassportNumber == passportNumber);

            return passengerItem;
        }
    }
}