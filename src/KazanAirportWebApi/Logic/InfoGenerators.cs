using System;

namespace KazanAirportWebApi.Logic
{
    public class InfoGenerators
    {
        /// <summary>
        /// Генерация номера билета
        /// </summary>
        /// <returns></returns>
        public static string GenerateTicketNumber()
        {
            var guid = Guid.NewGuid().ToString();
            return guid.Substring(0, 8).ToUpper();
        }

        /// <summary>
        /// Генерация места в салоне
        /// </summary>
        /// <returns></returns>
        public static string GenerateSeatNumber()
        {
            var seatLetters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            var random = new Random();

            return $"{random.Next(25)}{seatLetters[random.Next(4)]}";
        }
    }
}