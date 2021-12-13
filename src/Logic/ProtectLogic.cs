using System.Collections.Generic;
using KazanAirportWebApp.Models.Join_Models;

namespace KazanAirportWebApp.Logic
{
    /// <summary>
    /// Класс логики для защиты данных
    /// </summary>
    public class ProtectLogic
    {
        /// <summary>
        /// Чистка паролей пользователей, для отображения в окне администратора
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static List<UserItem> FilterPasswords(List<UserItem> users)
        {
            users.ForEach(x => x.passWord = null);
            return users;
        }
    }
}