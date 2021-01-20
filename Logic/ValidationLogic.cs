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
            var validationResult = "";

            using (var db = new KazanAirportDbEntities())
            {
                var loginIds = db.Database.SqlQuery<int>("Select id From dbo.Logins Where login = @login",
                    new SqlParameter("@login", user.login)).ToList();
                if (loginIds.Count != 0)
                    validationResult += "- Пользователь с таким логином уже зарегистрирован в системе\n";

                loginIds = db.Database.SqlQuery<int>("Select id From dbo.Logins Where email = @email",
                    new SqlParameter("@email", user.email)).ToList();
                if (loginIds.Count != 0)
                    validationResult += "- Пользователем с таким Email уже присутствует в системе\n";

            }

            return validationResult;
        }
    }
}