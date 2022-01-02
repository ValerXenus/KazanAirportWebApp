namespace KazanAirportWebApi.Models.JoinModels
{
    /// <summary>
    /// Класс данных по пассажиру с данными о привязанного пользователя
    /// </summary>
    public class PassengerItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Привязанный ID пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}