using System.Collections.Generic;
using KazanAirportWebApi.Models;

namespace KazanAirportWebApi.Logic
{
    /// <summary>
    /// Класс логики для защиты данных
    /// </summary>
    public class ProtectLogic
    {
        /// <summary>
        /// Чистка паролей пользователей, для отображения в окне администратора
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <returns></returns>
        public static List<DbUser> FilterPasswords(List<DbUser> users)
        {
            users.ForEach(x => x.UserPassword = null);
            return users;
        }
    }
}