using System.Collections.Generic;
using System.Text;
using KazanAirportWebApp.Models;

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
        /// <param name="users">Список пользователей</param>
        /// <returns></returns>
        public static List<DbUsers> FilterPasswords(List<DbUsers> users)
        {
            users.ForEach(x => x.UserPassword = null);
            return users;
        }

        /// <summary>
        /// Получить MD5-хэш строки
        /// </summary>
        /// <param name="input">Входящая строка</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();

            var inputBytes = Encoding.GetEncoding(1251).GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
                sb.Append(hashByte.ToString("X2"));

            return sb.ToString();
        }
    }
}