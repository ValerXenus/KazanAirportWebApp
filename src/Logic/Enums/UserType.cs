namespace KazanAirportWebApp.Logic.Enums
{
    /// <summary>
    /// Типы пользователей
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Пассажир (обычный пользователь)
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Сотрудник (оператор)
        /// </summary>
        Employee = 1,

        /// <summary>
        /// Администратор
        /// </summary>
        Admin = 2
    }
}